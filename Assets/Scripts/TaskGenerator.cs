using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    private Dictionary<string, GameObject[]> allInteractableObjects;
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
        SetUpAllData();
        SetUpEvents();

        ChooseAllTasksAtStart();
    }

    private void SetUpAllData()
    {
        gameObjectsWithTasks = new List<GameObject>();
        allInteractableObjects = new Dictionary<string, GameObject[]>();

        allInteractableObjects.Add(nameof(TaskObjectType.StreetLamp), GameObject.FindGameObjectsWithTag(nameof(TaskObjectType.StreetLamp)));
        allInteractableObjects.Add(nameof(TaskObjectType.ManHole), GameObject.FindGameObjectsWithTag(nameof(TaskObjectType.ManHole)));
        allInteractableObjects.Add(nameof(TaskObjectType.Tree), GameObject.FindGameObjectsWithTag(nameof(TaskObjectType.Tree)));
        allInteractableObjects.Add(nameof(TaskObjectType.SolarPanel), GameObject.FindGameObjectsWithTag(nameof(TaskObjectType.SolarPanel)));
    }

    private void SetUpEvents()
    {
        miniGameManager.OnGameOver += MiniGameManager_OnGameOver;
        miniGameManager.OnGameWon += MiniGameManager_OnGameWon;
    }

    private void AddTasksToObject(List<GameObject> allObjects, string objectType)
    {
        System.Random random = new System.Random();

        foreach(GameObject gameObject in allObjects)
        {
            List<GameObject> perfabs;
            GameObject gamePrefab;

            //Decided which game to display for each object
            switch (objectType)
            {
                case nameof(TaskObjectType.StreetLamp):
                    perfabs = GamePrefabs.Where(x => x.name.Contains("Rewire") || x.name.Contains("ColorBeep")).ToList();
                    gamePrefab = perfabs[random.Next(perfabs.Count)];
                    break;
                case nameof(TaskObjectType.ManHole):
                    perfabs = GamePrefabs.Where(x => x.name.Contains("Sewage")).ToList();
                    gamePrefab = perfabs[random.Next(perfabs.Count)];
                    break;
                case nameof(TaskObjectType.Tree):
                    perfabs = GamePrefabs.Where(x => x.name.Contains("Dig")).ToList();
                    gamePrefab = perfabs[random.Next(perfabs.Count)];
                    break;
                case nameof(TaskObjectType.SolarPanel):
                    perfabs = GamePrefabs.Where(x => x.name.Contains("Rewire") || x.name.Contains("ColorBeep")).ToList();
                    gamePrefab = perfabs[random.Next(perfabs.Count)];
                    break;
                default:
                    gamePrefab = GamePrefabs[random.Next(GamePrefabs.Length)];
                    break;

            }

            AddTaskToObject(gameObject, gamePrefab);
        }
    }

    private void AddTaskToObject(GameObject interactableObject, GameObject gamePrefab)
    {
        //Adds the script component
        InteractableTaskObject component = interactableObject.AddComponent<InteractableTaskObject>(); 
        component.GamePrefab = gamePrefab;

        //Changes the color
        interactableObject.GetComponent<MeshRenderer>().material = canSolveMaterial;
    }

    private void ChooseAllTasksAtStart()
    {
        //Chooses at random which object to give a task
        foreach(KeyValuePair<string, GameObject[]> pair in allInteractableObjects)
        {
            List<GameObject> allObjects = pair.Value.ToList();
            List<GameObject> allObjectsAdded = new List<GameObject>();

            int amount = 0;
            switch(pair.Key)
            {
                case nameof(TaskObjectType.StreetLamp):
                    amount = 15;
                    break;
                case nameof(TaskObjectType.ManHole):
                    amount = 8;
                    break;
                case nameof(TaskObjectType.Tree):
                    amount = 20;
                    break;
                case nameof(TaskObjectType.SolarPanel):
                    amount = 2;
                    break;
                default:
                    amount = 10;
                    break;
            }

            for (int i = 0; i < amount; i++)
            {
                int index = UnityEngine.Random.Range(0, allObjects.Count - 1);
                GameObject specificGameObject = allObjects[index];
                gameObjectsWithTasks.Add(specificGameObject);
                allObjects.RemoveAt(index);
                allObjectsAdded.Add(specificGameObject);
            }

            AddTasksToObject(allObjectsAdded, pair.Key);
        }
    }

    public void MiniGameManager_OnGameOver(InteractableTaskObject interactableObject)
    {
        //TODO: Arbitrary number
        if (interactableObject.AmountTries >= 2)
        {
            interactableObject.gameObject.GetComponent<MeshRenderer>().material = failedMaterial;
            Destroy(interactableObject);
        }

        interactableObject.AmountTries++;
    }

    public void MiniGameManager_OnGameWon(InteractableTaskObject interactableObject)
    {
        //TODO: Change to better stuff
        interactableObject.gameObject.GetComponent<MeshRenderer>().material = fixedMaterial;
        Destroy(interactableObject);
    }


}
