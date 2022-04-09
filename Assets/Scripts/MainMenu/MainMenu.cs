using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Localization.Settings;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        //TODO: Get save file, load safe file
        //Load lobby scene

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.Instance.LoadScene("Office");
    }

    public void NewGame()
    {
        //TODO: If save file exists, ask if it can be deleted (no --> go back to main menu)
        // Yes --> create new save file, start tutorial

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.Instance.LoadScene("Office");
    }
    public void OnLanguageChange(TMP_Dropdown dropdown)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value];
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
