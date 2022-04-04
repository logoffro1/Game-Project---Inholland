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
    //This class still needs implementation for deleting the icons.
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
            if (obj.enabled)
            {
                Vector3 taskVector = new Vector3(obj.transform.position.x, obj.transform.position.y + 20, obj.transform.position.z);
                GameObject prefab = Instantiate(imagePrefab, taskVector, imagePrefab.transform.localRotation, minimap.transform);
                allPrefabLocations.Add(prefab);
            }            
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
            if (prefab.gameObject.transform.position.x.ToString("F2")==vector.x.ToString("F2") &&
                prefab.gameObject.transform.position.y.ToString("F2") == vector.y.ToString("F2")&&
                prefab.gameObject.transform.position.z.ToString("F2") == vector.z.ToString("F2"))

            {

                return prefab;
            }
        }
        return null;
    }

    public void DeleteIcon(GameObject obj)
    {
        Destroy(GetPrefabByVector(new Vector3(obj.transform.position.x, obj.transform.position.y+20,obj.transform.position.z)));
        allPrefabLocations.Remove(GetPrefabByVector(new Vector3(obj.transform.position.x, obj.transform.position.y + 20, obj.transform.position.z)));
    }
}
    