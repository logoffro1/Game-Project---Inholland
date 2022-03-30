using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTaskObject : MonoBehaviour
{

    //This class is in the interactable object in the container

    public GameObject GamePrefab;
    private string hoverName;

    [HideInInspector]
    public int AmountTries = 0;

    [HideInInspector]
    public bool IsInteractable = true;

    [HideInInspector]
    public GameObject CurrentModel;

    [HideInInspector]
    private InteractableTaskStatusModels interactableTaskStatusModels;

    public TaskStatus Status;

    private void Start()
    {
        DetermineObject();
        SetInteractableTaskStatusModels();
    }

    private void SetInteractableTaskStatusModels()
    {
        if (gameObject.TryGetComponent(out InteractableTaskStatusModels childStatusModels))
        {
            interactableTaskStatusModels = childStatusModels;
        }
        else if (gameObject.transform.parent.gameObject.TryGetComponent(out InteractableTaskStatusModels statusModels))
        {
            interactableTaskStatusModels = statusModels;
        }
        else if (gameObject.transform.parent.gameObject.TryGetComponent(out InteractableTaskStatusModels parentstatusModels))
        {
            interactableTaskStatusModels = parentstatusModels;
        }
        else if (gameObject.transform.parent.parent.gameObject.TryGetComponent(out InteractableTaskStatusModels grandParentStatusModels))
        {
            interactableTaskStatusModels = grandParentStatusModels;
        }
    }



    public GameObject ChangeModel(TaskStatus status)
    {
        if (interactableTaskStatusModels == null) SetInteractableTaskStatusModels();
        Status = status;

        return interactableTaskStatusModels.ChangeModel(status);
    }



    public void DoAction(GameObject player)
    {
        if (IsInteractable)
        {
            MiniGameManager.Instance.StartGame(GamePrefab);
            MiniGameManager.Instance.InteractableObject = this;

            //TODO: Change until after game is finished?
            if (Status != TaskStatus.Touched)
            {
                ChangeModel(TaskStatus.Touched);
            }

            AmountTries++;
        }
        else
        {
            hoverName = string.Empty;
        }
    }

    public string GetHoverName() => hoverName;

    private void DetermineObject()
    {
        string tag = gameObject.tag;

        switch(tag)
        {
            case nameof(TaskObjectType.StreetLamp):
                hoverName = "Upgrade";
                break;
            case nameof(TaskObjectType.Tree):
                hoverName = "Plant";
                break;
            case nameof(TaskObjectType.ManHole):
                hoverName = "Clean";
                break;
            case nameof(TaskObjectType.SolarPanel):
                hoverName = "Fix";
                break;
            case nameof(TaskObjectType.RubbishBin):
                hoverName = "Build shingles";
                break;
            default:
                hoverName = "Interact";
                break;
        }
    }


}
