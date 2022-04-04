using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    private Dictionary<TaskObjectType, List<GameObject>> allInteractableObjects { get; set; }
    private Dictionary<TaskObjectType, List<GameObject>> allGamesToObjects;
    private Dictionary<TaskObjectType, int> allGamesToAmountSpawn;

    public GameObject[] GamePrefabs;
    public MiniGameManager miniGameManager;

    private List<GameObject> gameObjectsWithTasks;

    //Materials, will remove
    public Material canSolveMaterial;
    public Material fixedMaterial;
    public Material failedMaterial;


    // Start is called before the first frame update
    void Start()
    {
        gameObjectsWithTasks = new List<GameObject>();

        SetUpAllData();
        SetUpEvents();

        ChooseAllTasksAtStart();
    }

    private void SetUpAllData()
    {
        SetUpAllInteractableObjects();
        SetUpAllGamesToObjects();
        SetUpAllGamesToAmountSpawn();
    }

    private void SetUpAllInteractableObjects()
    {
        allInteractableObjects = new Dictionary<TaskObjectType, List<GameObject>>();

        //foreach object that has InteractableTaskObject script in it
    
        foreach (InteractableTaskStatusModels interactableContainers in FindObjectsOfType<InteractableTaskStatusModels>())
        {
            //Using tags
            TaskObjectType task = (TaskObjectType)Enum.Parse(typeof(TaskObjectType), interactableContainers.gameObject.tag);
            if (allInteractableObjects.ContainsKey(task))
            {
                allInteractableObjects[task].Add(interactableContainers.gameObject);
            }
            else allInteractableObjects.Add(task, new List<GameObject>());

            //Default is that nothing hasto be done //THIS IS WHERE THE NULL POINTER OCCURS
            //interactable.ChangeModel(TaskStatus.Success);
        }
    }

    private void SetUpAllGamesToObjects()
    {
        allGamesToObjects = new Dictionary<TaskObjectType, List<GameObject>>();

        //Manually set which object is linked to which game
        allGamesToObjects.Add(TaskObjectType.StreetLamp, GamePrefabs.Where(x => x.name.Contains("Rewire") || x.name.Contains("ColorBeep")).ToList());
        allGamesToObjects.Add(TaskObjectType.ManHole, GamePrefabs.Where(x => x.name.Contains("Sewage")).ToList());
        allGamesToObjects.Add(TaskObjectType.Tree, GamePrefabs.Where(x => x.name.Contains("Dig")).ToList());
        allGamesToObjects.Add(TaskObjectType.SolarPanel, GamePrefabs.Where(x => x.name.Contains("Solar")).ToList());
    }

    private void SetUpAllGamesToAmountSpawn()
    {
        allGamesToAmountSpawn = new Dictionary<TaskObjectType, int>();

        //Relative to amount there is
        foreach (TaskObjectType objectType in Enum.GetValues(typeof(TaskObjectType)))
        {
            if (allInteractableObjects.ContainsKey(objectType))
                allGamesToAmountSpawn.Add(objectType, allInteractableObjects[objectType].Count/3);
        }

        //Manual
        /*
        allGamesToAmountSpawn.Add(TaskObjectType.StreetLamp, 15);
        allGamesToAmountSpawn.Add(TaskObjectType.ManHole, 8);
        allGamesToAmountSpawn.Add(TaskObjectType.Tree, 20);
        allGamesToAmountSpawn.Add(TaskObjectType.SolarPanel, 2);
        */
    }

    private void SetUpEvents()
    {
        miniGameManager.OnGameOver += MiniGameManager_OnGameOver;
        miniGameManager.OnGameWon += MiniGameManager_OnGameWon;
    }

    private void AddTasksToObject(List<GameObject> allObjects, TaskObjectType objectType)
    {
        System.Random random = new System.Random();

        foreach(GameObject gameObject in allObjects)
        {
            //Get random game prefab for a game
            GameObject gamePrefab = allGamesToObjects[objectType][random.Next(allGamesToObjects[objectType].Count)];
            //Creating a fully functional interactable task object
            AddTaskToObject(gameObject, gamePrefab);
        }
    }

    private void AddTaskToObject(GameObject interactableContainers, GameObject gamePrefab)
    {
        //Enables the script component
        //Get the InteractableTaskObject from the container
        //InteractableTaskObject component = interactableContainers.GetComponentInChildren<InteractableTaskObject>();

        //Making it so that players can interact with it
        GameObject newTaskObject = interactableContainers.GetComponent<InteractableTaskStatusModels>().ChangeModel(TaskStatus.Untouched);
        newTaskObject.GetComponent<InteractableTaskObject>().GamePrefab = gamePrefab;
        newTaskObject.GetComponent<InteractableTaskObject>().enabled = true;

        //Changes the color TODO: REMOVE 
        foreach (MeshRenderer mesh in interactableContainers.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material = canSolveMaterial;
        }

    }

    private void ChooseAllTasksAtStart()
    {
        //Chooses at random which object to give a task
        //Goes through each tag (eg. first tree, then manhole, then streetlamp...)
        foreach(KeyValuePair<TaskObjectType, List<GameObject>> pair in allInteractableObjects)
        {
            //Initializing
            List<GameObject> allObjects = new List<GameObject>(pair.Value);
            List<GameObject> allObjectsAdded = new List<GameObject>();
            int amount = allGamesToAmountSpawn[pair.Key];

            for (int i = 0; i < amount; i++)
            {
                //Random int
                int index = UnityEngine.Random.Range(0, allObjects.Count);

                //Get a random task from list
                GameObject oneTaskObject = allObjects[index];
                gameObjectsWithTasks.Add(oneTaskObject);

                //For logic
                allObjects.RemoveAt(index);
                allObjectsAdded.Add(oneTaskObject);
            }

            AddTasksToObject(allObjectsAdded, pair.Key);
        }
    }

    public event Action OnStopInteraction;

    public void MiniGameManager_OnGameOver(InteractableTaskObject interactableObject)
    {
        //TODO: Arbitrary number
        if (interactableObject.AmountTries >= 2)
        {
            //GameIsOver(interactableObject, failedMaterial);
            interactableObject.ChangeModel(TaskStatus.Fail);
        }
    }

    public void MiniGameManager_OnGameWon(InteractableTaskObject interactableObject)
    {
        //TODO: Change to better stuff
        //GameIsOver(interactableObject, fixedMaterial);
        interactableObject.ChangeModel(TaskStatus.Success);

    }

    private void GameIsOver(InteractableTaskObject interactableObject, Material material)
    {
        //interactableObject.gameObject.GetComponent<MeshRenderer>().material = material;
        interactableObject.enabled = false;
        interactableObject.IsInteractable = false;
    }


}
