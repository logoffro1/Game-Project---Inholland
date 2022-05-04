using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCreation : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject[] lobbyDeactivation;

    public void OnClick_Back()
    {
        mainMenu.SetActive(true);
        foreach(GameObject go in lobbyDeactivation)
        {
            go.SetActive(false);
        }
    }

}
