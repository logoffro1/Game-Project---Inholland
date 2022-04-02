using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MinimapScript : MonoBehaviour
{
    //Needs to be private after pitch
    private  List<InteractableTaskObject> allInteractableObjects;
    public  List<GameObject> allPrefabLocations;
    public Transform player;
    public GameObject imagePrefab;
    public GameObject minimap;
    //InteractableTaskStatusModels interactableContainers in FindObjectsOfType<InteractableTaskStatusModels>()
    private void Start()
    {
        allInteractableObjects = new List<InteractableTaskObject>();
        allPrefabLocations = new List<GameObject>();
        initializeTaskList();
        PlantAllQuestIcons();
    }
    private void LateUpdate()
    {
        
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    private void PlantAllQuestIcons()
    {
        foreach(InteractableTaskObject obj in allInteractableObjects)
        {
            Vector3 taskVector = new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y + 20, obj.gameObject.transform.position.z);
            GameObject prefab = Instantiate(imagePrefab,taskVector,imagePrefab.transform.localRotation,minimap.transform);
            allPrefabLocations.Add(prefab);
            
        }
    }

    private void initializeTaskList()
    {
        foreach (InteractableTaskObject interactableContainers in FindObjectsOfType<InteractableTaskObject>())
        {
            allInteractableObjects.Add(interactableContainers);
        }
    }

    private GameObject GetPrefabByVector(Vector3 vector)
    {
        foreach(GameObject prefab in allPrefabLocations)
        {
            if(new Vector3(prefab.transform.position.x, prefab.transform.position.y, prefab.transform.position.z) == vector)
            {
                return prefab;
            }
        }
        return null;
    }

    public  void DeleteIcon(Vector3 vector)
    {
        clearAllIcons();
        allPrefabLocations.Remove(GetPrefabByVector(vector));
        updateIcons();
    }

    private void clearAllIcons() { 
        foreach(GameObject go in allPrefabLocations)
        {
            Destroy(go);
        }   
    }
    private void updateIcons()
    {
        foreach (GameObject go in allPrefabLocations)
        {
            Instantiate(go);
        }
    }
}
    