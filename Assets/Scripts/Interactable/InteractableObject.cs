using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractableObject
{
    public GameObject GamePrefab;
    private string hoverName;

    //private bool isSuccessful = false;
    //private int amountTries = 0;

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
            case "StreetLamp":
                hoverName = "Upgrade";
                break;
            case "Tree":
                hoverName = "Plant";
                break;
            case "ManHole":
                hoverName = "Clean";
                break;
            default:
                hoverName = "Interact";
                break;
        }
    }

}
