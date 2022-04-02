using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapScript : MonoBehaviour
{
    //Needs to be private after pitch
    private  List<InteractableTaskObject> allInteractableObjects;
    public Transform player;
    public GameObject imagePrefab;
    public GameObject minimap;
    //InteractableTaskStatusModels interactableContainers in FindObjectsOfType<InteractableTaskStatusModels>()
    private void Start()
    {
        allInteractableObjects = new List<InteractableTaskObject>();
        
        initializeTaskList();

        PlantAllQuestIcons();
    }
    private void LateUpdate()
    {
        
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

/*        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
*/    }

    private void PlantAllQuestIcons()
    {
        foreach(InteractableTaskObject obj in allInteractableObjects)
        {
            if(obj.Status!=TaskStatus.Success)
            Instantiate(imagePrefab,new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y+20, obj.gameObject.transform.position.z),imagePrefab.transform.localRotation,minimap.transform);
        }
    }

    private void initializeTaskList()
    {

        foreach (InteractableTaskObject interactableContainers in FindObjectsOfType<InteractableTaskObject>())
        {
            allInteractableObjects.Add(interactableContainers);
        }
    }
}
    