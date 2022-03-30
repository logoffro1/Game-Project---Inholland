using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour,IInteractableObject
{
    public GameObject diggingGamePrefab;
    public void DoAction(GameObject player)
    {
        MiniGameManager.Instance.StartGame(diggingGamePrefab);
    }

    public string GetHoverName() => "Care";


}
