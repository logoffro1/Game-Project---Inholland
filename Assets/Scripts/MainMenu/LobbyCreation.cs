using UnityEngine;

public class LobbyCreation : MonoBehaviour // change the canvas shown
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject[] lobbyDeactivation;

    public void OnClick_Back() // when clicking on back button, show the main menu
    {
        mainMenu.SetActive(true);
        foreach(GameObject go in lobbyDeactivation)
        {
            go.SetActive(false);
        }
    }

}
