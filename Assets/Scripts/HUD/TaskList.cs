using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TaskList : MonoBehaviour
{
    int solarCounter = 0;
    int treeCounter = 0;
    int lampCounter = 0;
    int sewerCounter = 0;
    int totalObjects = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindObjects();
        Debug.Log("Shingles: "+ solarCounter);
        Debug.Log("Manhole: " + sewerCounter);
        Debug.Log("Trees: " + treeCounter);
        Debug.Log("Street lamps: " + lampCounter);
        Debug.Log("Total tasks: " + totalObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                if (task.transform.CompareTag("StreetLamp"))
                {
                    lampCounter++;
                }
                Debug.Log(task.tag);
                totalObjects++;
                list.Add(task);
            }
        }
        return list;
    }

    void AssignTasks()
    {
        //pick 3 random tasks between shingles/manhole/trees/streetlamps, then assign a number between 1 and half the total of that task type to a player


    }
}
