using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class InteractableTaskStatusModels : MonoBehaviour
{

    // This class is in the container

    public GameObject UntouchedPrefabModel;
    public GameObject TouchedPrefabModel;
    public GameObject SuccessPrefabModel;
    public GameObject FailPrefabModel;

    private GameObject InstantiateModel(TaskStatus status, GameObject child)
    {
        GameObject modelPrefab = UntouchedPrefabModel;

        switch (status)
        {
            case TaskStatus.Success:
                modelPrefab = SuccessPrefabModel;
                break;
            case TaskStatus.Fail:
                modelPrefab = FailPrefabModel;
                break;
            case TaskStatus.Touched:
                modelPrefab = TouchedPrefabModel;
                break;
            case TaskStatus.Untouched:
                modelPrefab = UntouchedPrefabModel;
                break;
        }

        //Put the interactable object in this container (become child of gameobject of this place)
        GameObject gameObject = Instantiate(modelPrefab, transform.position, transform.rotation, transform);

        //If Touched or faUntouchedil, should add all of these things
        if (status == TaskStatus.Touched || status == TaskStatus.Untouched)
        {
            //Gets all necessary info
            InteractableTaskObject interactable = gameObject.GetComponent<InteractableTaskObject>();
            interactable.SetLocalizedString(GameObject.Find("HoverText").GetComponent<LocalizeStringEvent>());
            try
            {
                interactable.enabled = true;
                interactable.IsInteractable = true;
                interactable.Status = status;
            }catch(Exception e)
            {
                Debug.Log(e.Message);
            }

            if(child.TryGetComponent(out InteractableTaskObject interactableTaskObject))
            {
                interactable.GamePrefab = interactableTaskObject.GamePrefab;
            }
        }

        return gameObject;
    }

    public GameObject ChangeModel(TaskStatus status)
    {
        GameObject child = transform.GetChild(0).gameObject;

        //Destroys old model
        Destroy(child);

        //TODO: EDIT
        if (status == TaskStatus.Success)
        {
            TaskList taskList = FindObjectOfType<TaskList>();
            taskList.TaskWon(this);
        }

        //Instaiates new model
        GameObject newModel = InstantiateModel(status, child);

        if (status == TaskStatus.Success || status == TaskStatus.Fail)
        {
            MinimapScript ms = GameObject.FindObjectOfType<MinimapScript>();
            ms.DeleteIcon(newModel);
        }

        return newModel;
    }
}
