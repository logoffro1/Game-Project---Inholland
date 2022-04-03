using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    //InteractableTaskObject interactable = gameObject.GetComponent<InteractableTaskObject>();
    int solarCounter = 0;
    int plantTreeCounter = 0;
    int memoryGameCounter = 0;
    int sewerCounter = 0;
    int wireCounter = 0;
    int totalObjects = 0;
    // Start is called before the first frame update
    void Start()
    {
        FindObjects();
        Debug.Log("Shingles: "+ solarCounter);
        Debug.Log("Manhole: " + sewerCounter);
        Debug.Log("Trees: " + plantTreeCounter);
        Debug.Log("Street lamps: " + memoryGameCounter);
        Debug.Log("Total tasks: " + totalObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<InteractableTaskObject> FindObjects()
    {
        List<InteractableTaskObject> list = new List<InteractableTaskObject>();

        foreach (InteractableTaskObject task in FindObjectsOfType<InteractableTaskObject>())
        {
            if (task.tag == "SolarPanel")
            {
                solarCounter++;
            }
            if(task.tag == "ManHole")
            {
                sewerCounter++;
            }
            if (task.tag == "Tree")
            {
                plantTreeCounter++;
            }
            if (task.transform.tag == "StreetLamp")
            {
                memoryGameCounter++;
            }
            Debug.Log(task.tag);
            totalObjects++;
            list.Add(task);
        }
        return list;
    }
}
