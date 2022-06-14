using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using Photon.Pun;
using System;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] lobbyActivation;
    [SerializeField] private TMP_InputField nicknameInput;

    private void Start()
    {
        if (PhotonNetwork.NetworkClientState == Photon.Realtime.ClientState.Leaving)
        {
            return;
        }
            if (PlayerPrefs.HasKey("nickname")) // set the nickname if nickname exists
            {
          
                try
                {
                    nicknameInput.text = PlayerPrefs.GetString("nickname");
                }
                catch(Exception e)
                {
                    nicknameInput.text = "New Player";
                }
            }
    }
    public void NewGame()
    {
        //set player nickname and show lobby canvas
        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;
        foreach (GameObject go in lobbyActivation)
            go.SetActive(true);

        gameObject.SetActive(false);
        PhotonNetwork.NickName = nicknameInput.text;
    }
    public void OnLanguageChange(TMP_Dropdown dropdown) // change localization settings based on language
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value];
    }
    public void OnUserNameChange(TextMeshProUGUI nickname) // constantly update the nickname and save it
    {
        if (nickname.text.Length <= 1)
            PhotonNetwork.NickName = MasterManager.GameSettings.NickName;

        PhotonNetwork.NickName = nickname.text;
        PlayerPrefs.SetString("nickname", nickname.text);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
