using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTaskObject : MonoBehaviour
{
    public GameObject GamePrefab;
    private string hoverName;

    public int AmountTries = 0;

    private void Start()
    {
        DetermineObject();
    }

    public void DoAction(GameObject player)
    {
        MiniGameManager.Instance.StartGame(GamePrefab);
        MiniGameManager.Instance.InteractableObject = this;
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
            default:
                hoverName = "Interact";
                break;
        }
    }

}
