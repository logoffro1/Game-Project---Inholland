using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTaskObject : MonoBehaviour
{
    public GameObject GamePrefab;
    private string hoverName;

    [HideInInspector]
    public int AmountTries = 0;

    [HideInInspector]
    public bool IsInteractable = true;

    [HideInInspector]
    public GameObject CurrentModel;

    [HideInInspector]
    public InteractableTaskStatusModels interactableTaskStatusModels;

    public TaskStatus Status;

    private void Start()
    {
        DetermineObject();
        interactableTaskStatusModels = gameObject.GetComponentInParent<InteractableTaskStatusModels>();
    }

    public void ChangeModel(TaskStatus status)
    {
        interactableTaskStatusModels.ChangeModel(status);
        Status = status;
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
