using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SolarPanel : MonoBehaviour, IInteractableObject
{
    public GameObject RewireGamePrefab;
    public void DoAction(GameObject player)
    {
        MiniGameManager.Instance.StartGame(RewireGamePrefab);
    }

    public string GetHoverName() => "Solar Panel";
}
