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
    public Material tmpMaterial;

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

        allInteractableObjects.Add("StreetLamp", GameObject.FindGameObjectsWithTag("StreetLamp"));
        allInteractableObjects.Add("ManHole", GameObject.FindGameObjectsWithTag("ManHole"));
        allInteractableObjects.Add("Tree", GameObject.FindGameObjectsWithTag("Tree"));
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
                case "StreetLamp":
                    perfabs = GamePrefabs.Where(x => x.name.Contains("Rewire") || x.name.Contains("ColorBeep")).ToList();
                    gamePrefab = perfabs[random.Next(perfabs.Count -1)];
                    break;
                case "ManHole":
                    perfabs = GamePrefabs.Where(x => x.name.Contains("Sewage")).ToList();
                    gamePrefab = perfabs[random.Next(perfabs.Count -1)];
                    break;
                case "Tree":
                    perfabs = GamePrefabs.Where(x => x.name.Contains("Dig")).ToList();
                    gamePrefab = perfabs[random.Next(perfabs.Count - 1)];
                    break;
                default:
                    gamePrefab = GamePrefabs[random.Next(GamePrefabs.Length -1)];
                    break;

            }

            AddTaskToObject(gameObject, gamePrefab);
        }
    }

    private void AddTaskToObject(GameObject interactableObject, GameObject gamePrefab)
    {
        //Adds the script component
        InteractableObject component = interactableObject.AddComponent<InteractableObject>(); 
        component.GamePrefab = gamePrefab;

        //Changes the color
        interactableObject.GetComponent<MeshRenderer>().material = tmpMaterial;
    }

    private void ChooseAllTasksAtStart()
    {
        //Chooses at random which object to give a task
        foreach(KeyValuePair<string, GameObject[]> pair in allInteractableObjects)
        {
            List<GameObject> allObjects = pair.Value.ToList();
            List<GameObject> allObjectsAdded = new List<GameObject>();
            for (int i = 0; i < 10; i++)
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

    public void MiniGameManager_OnGameOver(InteractableObject interactableObject)
    {
        //TODO
        //interactableObject.gameObject.GetComponent<MeshRenderer>().material = tmpMaterial;
        //Destroy(interactableObject);
    }

    public void MiniGameManager_OnGameWon(InteractableObject interactableObject)
    {
        //TODO: Change to better stuff
        interactableObject.gameObject.GetComponent<MeshRenderer>().material = null;
        Destroy(interactableObject);
    }


}
