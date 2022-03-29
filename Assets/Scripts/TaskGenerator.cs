using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    private Dictionary<TaskObjectType, List<GameObject>> allInteractableObjects;
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
        foreach (InteractableTaskObject interactable in FindObjectsOfType<InteractableTaskObject>())
        {
            //Using tags
            TaskObjectType task = (TaskObjectType)Enum.Parse(typeof(TaskObjectType), interactable.gameObject.tag);
            if (allInteractableObjects.ContainsKey(task))
            {
                allInteractableObjects[task].Add(interactable.gameObject);
            }
            else allInteractableObjects.Add(task, new List<GameObject>());
        }
    }

    private void SetUpAllGamesToObjects()
    {
        allGamesToObjects = new Dictionary<TaskObjectType, List<GameObject>>();

        //Manually set which object is linked to which game
        allGamesToObjects.Add(TaskObjectType.StreetLamp, GamePrefabs.Where(x => x.name.Contains("ColorBeep")).ToList());
        //allGamesToObjects.Add(TaskObjectType.StreetLamp, GamePrefabs.Where(x => x.name.Contains("Rewire") || x.name.Contains("ColorBeep")).ToList());
        allGamesToObjects.Add(TaskObjectType.ManHole, GamePrefabs.Where(x => x.name.Contains("Sewage")).ToList());
        allGamesToObjects.Add(TaskObjectType.Tree, GamePrefabs.Where(x => x.name.Contains("Dig")).ToList());
        allGamesToObjects.Add(TaskObjectType.SolarPanel, GamePrefabs.Where(x => x.name.Contains("Rewire") || x.name.Contains("ColorBeep")).ToList());
        allGamesToObjects.Add(TaskObjectType.RubbishBin, GamePrefabs.Where(x => x.name.Contains("Solar")).ToList());
    }

    private void SetUpAllGamesToAmountSpawn()
    {
        allGamesToAmountSpawn = new Dictionary<TaskObjectType, int>();

        //Relative to amount there is
        foreach (TaskObjectType objectType in Enum.GetValues(typeof(TaskObjectType)))
        {
            allGamesToAmountSpawn.Add(objectType, allInteractableObjects[objectType].Count);
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
            GameObject gamePrefab = allGamesToObjects[objectType][random.Next(allGamesToObjects[objectType].Count)];
            AddTaskToObject(gameObject, gamePrefab);
        }
    }

    private void AddTaskToObject(GameObject interactableObject, GameObject gamePrefab)
    {
        //Enables the script component
        InteractableTaskObject component = interactableObject.GetComponent<InteractableTaskObject>();
        component.GamePrefab = gamePrefab;
        component.enabled = true;

        //Changes the color
        //interactableObject.GetComponent<MeshRenderer>().material = canSolveMaterial;
    }

    private void ChooseAllTasksAtStart()
    {
        //Chooses at random which object to give a task
        foreach(KeyValuePair<TaskObjectType, List<GameObject>> pair in allInteractableObjects)
        {
            List<GameObject> allObjects = new List<GameObject>(pair.Value);
            List<GameObject> allObjectsAdded = new List<GameObject>();

            int amount = allGamesToAmountSpawn[pair.Key];
            

            for (int i = 0; i < amount; i++)
            {
                int index = UnityEngine.Random.Range(0, allObjects.Count);
                GameObject specificGameObject = allObjects[index];
                gameObjectsWithTasks.Add(specificGameObject);
                allObjects.RemoveAt(index);
                allObjectsAdded.Add(specificGameObject);
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
