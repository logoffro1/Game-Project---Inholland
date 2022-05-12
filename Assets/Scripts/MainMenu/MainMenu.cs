using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Localization.Settings;
using Photon.Pun;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] lobbyActivation;
    [SerializeField] private TMP_InputField nicknameInput;

    private void Start()
    {
        if (PlayerPrefs.HasKey("nickname"))
        {
            Debug.Log("NICKNAME");
            nicknameInput.text = PlayerPrefs.GetString("nickname");
        }
    }


    public void NewGame()
    {
        //TODO: If save file exists, ask if it can be deleted (no --> go back to main menu)
        // Yes --> create new save file, start tutorial
        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;
        //LevelManager.Instance.LoadScene("Office");
        foreach (GameObject go in lobbyActivation)
            go.SetActive(true);

        gameObject.SetActive(false);
        PhotonNetwork.NickName = nicknameInput.text;
    }
    public void OnLanguageChange(TMP_Dropdown dropdown)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value];
    }
    public void OnUserNameChange(TextMeshProUGUI nickname)
    {
        if (nickname.text.Length <= 1)
            PhotonNetwork.NickName = MasterManager.GameSettings.NickName;

        PhotonNetwork.NickName = nickname.text;
        PlayerPrefs.SetString("nickname", nickname.text);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
