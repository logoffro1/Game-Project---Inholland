using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

public class TaskList : MonoBehaviour
{
    bool IsMinimumRequirementMet { get => taskObjectTypes.Values.Any(value => value[0] == value[1]); }
    bool IsTaskListComplete { get => taskObjectTypes.Values.All(value => value[0] == value[1]); }

    bool bonusSusPointsAwarded = false;
    int solarCounter = 0;
    int treeCounter = 0;
    int lampCounter = 0;
    int sewerCounter = 0;
    int totalObjects = 0;
    int bonusSusPoints = 5;

    List<InteractableTaskStatusModels> tasks = new List<InteractableTaskStatusModels>();
    List<InteractableTaskStatusModels> allTasks = new List<InteractableTaskStatusModels>();
    Dictionary<TaskObjectType,int[]> taskObjectTypes = new Dictionary<TaskObjectType,int[]>();
    Dictionary<TaskObjectType, string> taskStrings = new Dictionary<TaskObjectType,string>()
    {
        { TaskObjectType.Tree, "Plant Trees" },
        { TaskObjectType.SolarPanel, "Upgrade solar panel" },
        { TaskObjectType.StreetLamp, "Upgrade street lamp" },
        { TaskObjectType.ManHole, "Clean sewers" }
    };
    private TextMeshProUGUI taskListText;

    // Start is called before the first frame update
    void Start()
    {
        FindObjects();
        Debug.Log("Shingles: "+ solarCounter);
        Debug.Log("Manhole: " + sewerCounter);
        Debug.Log("Trees: " + treeCounter);
        Debug.Log("Street lamps: " + lampCounter);
        Debug.Log("Total tasks: " + totalObjects);
        AssignTasks();
        taskListText = GetComponent<TextMeshProUGUI>();
        //MiniGameManager.Instance.OnGameWon += OnTaskWon;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTaskListComplete)
        {
            Debug.Log("Minimum requirement and task list is complete");
        }
    }

    List<InteractableTaskStatusModels> FindObjects()
    {
        List<InteractableTaskStatusModels> list = new List<InteractableTaskStatusModels>();
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
                totalObjects++;
                list.Add(task);
            }
        }
        return list;
    }

    void AssignTasks()
    {
        List<TaskObjectType> allTypes = ((TaskObjectType[])Enum.GetValues(typeof(TaskObjectType))).ToList();
        
        for (int i = 0; i < 3; i++)
        {
            TaskObjectType type = allTypes[UnityEngine.Random.Range(0,allTypes.Count)];
            if (!taskObjectTypes.ContainsKey(type))
            {
                taskObjectTypes.Add(type,new int[2] {0,UnityEngine.Random.Range(2, 6)});
                allTypes.Remove(type);
            }
        }
        /*foreach(KeyValuePair<TaskObjectType, int> pair in taskObjectTypes)
        {
            Debug.Log(pair.Key.ToString() + pair.Value);
        }*/
    }

    void UpdateText()
    {
        List<string> list = new List<string>();
        foreach (KeyValuePair<TaskObjectType, int[]> kvp in taskObjectTypes)
        {
            list.Add($"{taskStrings[kvp.Key]} ({kvp.Value[0]}/{kvp.Value[1]})");
        }
        taskListText.text = string.Join("\n", list);
    }

    public void TaskWon(InteractableTaskStatusModels task)
    {
        //if conditions below need to check what task is being completed, as task.tag is already destroyed when checking after the task completes
        Debug.Log("Task finished");

        if (Enum.TryParse(task.tag, out TaskObjectType objectType) && taskObjectTypes.ContainsKey(objectType))
        {
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
            if (task.tag == "StreetLamp")
            {
                taskObjectTypes[TaskObjectType.StreetLamp][0]++;
            }
            UpdateText();
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
