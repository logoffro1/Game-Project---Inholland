using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPost : MonoBehaviour, IInteractableObject
{
    public GameObject rewireGamePrefab;
    public void DoAction(GameObject player)
    {
        MiniGameManager.Instance.StartGame(rewireGamePrefab);
        //GetComponent<Material>().color = Color.red;
    }

    public string GetHoverName() => "Upgrade";
}
