using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

public class TaskList : MonoBehaviour
{
    //minimum requirement of contribution property
    bool IsMinimumRequirementMet { get => taskObjectTypes.Values.Any(value => value[0] == value[1]); }
    //task list complete property
    bool IsTaskListComplete { get => taskObjectTypes.Values.All(value => value[0] == value[1]); }
    //declaring variables
    bool bonusSusPointsAwarded = false;
    int solarCounter = 0;
    int treeCounter = 0;
    int lampCounter = 0;
    int sewerCounter = 0;
    int windTurbineCounter = 0;
    int binCounter = 0;
    int totalObjects = 0;
    int bonusSusPoints = 5;
    //list of interactabletaskstatusmodels of assigned tasks and all possible tasks
    List<InteractableTaskStatusModels> tasks = new List<InteractableTaskStatusModels>();
    List<InteractableTaskStatusModels> allTasks = new List<InteractableTaskStatusModels>();
    //dictionary of assigned tasks and task names
    Dictionary<TaskObjectType, int[]> taskObjectTypes = new Dictionary<TaskObjectType, int[]>();
    Dictionary<TaskObjectType, string> taskStrings;
    private TextMeshProUGUI taskListText;

    //task list localization variables
    private LocalizeStringEvent localizedStringEvent;
    [SerializeField] private LocalizedString[] localizedStrings;
    private LocalizationSettings locSettings;
    // Start is called before the first frame update
    void Start()
    {
        //task list string names are filled into the dictionary
        taskStrings = new Dictionary<TaskObjectType, string>()
             {
        { TaskObjectType.Tree, "Plant trees"},
        { TaskObjectType.SolarPanel, "Set up solar panel" },
        { TaskObjectType.StreetLamp, "Upgrade street lamp" },
        { TaskObjectType.ManHole, "Clean sewers" },
        { TaskObjectType.Bin, "Recycle items" },
        { TaskObjectType.WindTurbine, "Weld Turbine fans" }        
        };
        //localization is checked as this is a dynamic list that can change
        localizedStringEvent = GetComponent<LocalizeStringEvent>();

        localizedStringEvent.OnUpdateString.AddListener(OnStringChanged);
        UpdateLocalization();
        //finds all objects on the level, then assigns tasks to each player
        FindObjects();

        AssignTasks();
        taskListText = GetComponent<TextMeshProUGUI>();
        //MiniGameManager.Instance.OnGameWon += OnTaskWon;
        UpdateText();
    }

    //checks when the selected locale is changed to update the language of the task list
    private void LocSettings_OnSelectedLocaleChanged(Locale obj)
    {
        UpdateLocalization();
    }

    private void OnStringChanged(string s)
    {
        if (locSettings == null) return;
        UpdateLocalization();
    }
    //handles the updating of localization when language is changed, as the task list is filled through code
    //and not the unity inspector, so a localize string event component cannot be used
    private async void UpdateLocalization()
    {


        var handle = LocalizationSettings.InitializationOperation;
        await handle.Task;
        locSettings = handle.Result;
        locSettings.OnSelectedLocaleChanged += LocSettings_OnSelectedLocaleChanged;

        int count = 0;
        foreach (TaskObjectType type in taskStrings.Keys.ToList())
        {
            taskStrings[type] = locSettings
                .GetStringDatabase()
                .GetLocalizedString(localizedStrings[count].TableReference, localizedStrings[count].TableEntryReference);
            count++;
        }
        UpdateText();
        
    }
    /*   // Update is called once per frame
       void Update()
       {
           if (IsTaskListComplete)
           {
               Debug.Log("Minimum requirement and task list is complete");
           }
       }
   */

    //searches the map for all task objects
    List<InteractableTaskStatusModels> FindObjects()
    {
        //declares a new list of tasks
        List<InteractableTaskStatusModels> list = new List<InteractableTaskStatusModels>();
        //checks each task in the map which have the untouched status, meaning they are active and
        //increments the counters for each type of task
        //and fills the list as well as the total objects
        foreach (InteractableTaskStatusModels task in FindObjectsOfType<InteractableTaskStatusModels>().Where(x => x.gameObject.GetComponentInChildren<InteractableTaskObject>() && x.gameObject.GetComponentInChildren<InteractableTaskObject>().Status == TaskStatus.Untouched))
        {
            if (task != null && task.enabled)
            {
                if (task.tag == "SolarPanel")
                {
                    solarCounter++;
                }
                if (task.tag == "ManHole")
                {
                    sewerCounter++;
                }
                if (task.tag == "Tree")
                {
                    treeCounter++;
                }
                if (task.tag == "StreetLamp")
                {
                    lampCounter++;
                }
                if (task.tag == "WindTurbine")
                {
                    windTurbineCounter++;
                }
                if (task.tag == "Bin")
                {
                    binCounter++;
                }
                totalObjects++;
                list.Add(task);
            }
        }
        return list;
    }

    void AssignTasks()
    {
        //all types of tasks are fetched and are assigned per player
        List<TaskObjectType> allTypes = ((TaskObjectType[])Enum.GetValues(typeof(TaskObjectType))).ToList();
        List<TaskObjectType> cityTypes = ((TaskObjectType[])Enum.GetValues(typeof(TaskObjectType))).ToList();
        cityTypes.Remove(TaskObjectType.WindTurbine);

        for (int i = 0; i < 3; i++)
        {
            if (SceneManager.GetActiveScene().name == "CityCenter" || SceneManager.GetActiveScene().name == "GameUKDay")
            //randomly picks a type of task, then randomly assigns an amount of them to do between 2 and 6
            //which is then removed from the list and done again 3 times
            TaskObjectType type = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
            if (!taskObjectTypes.ContainsKey(type))
            {
                TaskObjectType type = cityTypes[UnityEngine.Random.Range(0, cityTypes.Count)];
                if (!taskObjectTypes.ContainsKey(type))
                {
                    taskObjectTypes.Add(type, new int[2] { 0, UnityEngine.Random.Range(2, 6) });
                    cityTypes.Remove(type);
                }
            }
            else
            {
                TaskObjectType type = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
                if (!taskObjectTypes.ContainsKey(type))
                {
                    taskObjectTypes.Add(type, new int[2] { 0, UnityEngine.Random.Range(2, 6) });
                    allTypes.Remove(type);
                }
            }
        }
        /*foreach(KeyValuePair<TaskObjectType, int> pair in taskObjectTypes)
        {
            Debug.Log(pair.Key.ToString() + pair.Value);
        }*/
    }

    //when a task is completed, the counter is incremented to reflect that on the task list interface
    void UpdateText()
    {
        List<string> list = new List<string>();
        foreach (KeyValuePair<TaskObjectType, int[]> kvp in taskObjectTypes)
        {
            list.Add($"{taskStrings[kvp.Key]} ({kvp.Value[0]}/{kvp.Value[1]})");
        }
        if (list.Count > 0)
            taskListText.text = string.Join("\n", list);
    }

    //checks which task has been won and if a task type has been completed, or if the entire task list has been completed
    //and awards sustainability points based on that
    public void TaskWon(InteractableTaskStatusModels task)
    {
        //if conditions below need to check what task is being completed, as task.tag is already destroyed when checking after the task completes
        Debug.Log("Task finished");

        if (Enum.TryParse(task.tag, out TaskObjectType objectType) && taskObjectTypes.ContainsKey(objectType))
        {
            //depending on the type of task,the task type counter in the task object types dictionary is incremented unless it
            //is greater than the total number of task type assigned
            if (task.tag == "SolarPanel")
            {

                if (taskObjectTypes[TaskObjectType.SolarPanel][0] + 1 <= taskObjectTypes[TaskObjectType.SolarPanel][1])
                    taskObjectTypes[TaskObjectType.SolarPanel][0]++;
            }
            if (task.tag == "ManHole")
            {
                if (taskObjectTypes[TaskObjectType.ManHole][0] + 1 <= taskObjectTypes[TaskObjectType.ManHole][1])
                    taskObjectTypes[TaskObjectType.ManHole][0]++;

            }
            if (task.tag == "Tree")
            {
                if (taskObjectTypes[TaskObjectType.Tree][0] + 1 <= taskObjectTypes[TaskObjectType.Tree][1])
                    taskObjectTypes[TaskObjectType.Tree][0]++;
            }
            if (task.tag == "StreetLamp")
            {
                if (taskObjectTypes[TaskObjectType.StreetLamp][0] + 1 <= taskObjectTypes[TaskObjectType.StreetLamp][1])
                    taskObjectTypes[TaskObjectType.StreetLamp][0]++;
            }
            if (task.tag == "WindTurbine")
            {
                if (taskObjectTypes[TaskObjectType.WindTurbine][0] + 1 <= taskObjectTypes[TaskObjectType.WindTurbine][1])
                    taskObjectTypes[TaskObjectType.WindTurbine][0]++;
            }
            if (task.tag == "Bin")
            {
                if (taskObjectTypes[TaskObjectType.Bin][0] + 1 <= taskObjectTypes[TaskObjectType.Bin][1])
                    taskObjectTypes[TaskObjectType.Bin][0]++;
            }
            //updates text of the task list
            UpdateText();
            //if the task list is complete and the sustainability points have not been awared, they are awarded to the player
            //and the sustainability of the level is increased
            if (IsTaskListComplete && !bonusSusPointsAwarded)
            {
                ProgressBar.Instance.ChangeSustainibility(bonusSusPoints, true);
                bonusSusPointsAwarded = true;
            }
        }
    }

    /*
    private void OnTaskWon(InteractableTaskObject task)
    {
        //if conditions below need to check what task is being completed, as task.tag is already destroyed when checking after the task completes
        Debug.Log("Task finished");        
        if (task.tag == "SolarPanel")
        {
            taskObjectTypes[TaskObjectType.SolarPanel][0]++;
        }
        if (task.tag == "ManHole")
        {
            taskObjectTypes[TaskObjectType.ManHole][0]++;
        }
        if (task.tag == "Tree")
        {
            taskObjectTypes[TaskObjectType.Tree][0]++;
        }
        if (task.transform.CompareTag("StreetLamp"))
        {
            taskObjectTypes[TaskObjectType.StreetLamp][0]++;
        }
        UpdateText();
    }
    */

}
