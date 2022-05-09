using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Localization.Settings;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] lobbyActivation;
    public void ContinueGame()
    {
        //TODO: Get save file, load safe file
        //Load lobby scene

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.Instance.LoadScenePhoton("Office",false);
    }

    public void NewGame()
    {
        //TODO: If save file exists, ask if it can be deleted (no --> go back to main menu)
        // Yes --> create new save file, start tutorial

        //LevelManager.Instance.LoadScene("Office");
        foreach (GameObject go in lobbyActivation)
            go.SetActive(true);

        gameObject.SetActive(false);
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
        Debug.Log(nickname.text);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
