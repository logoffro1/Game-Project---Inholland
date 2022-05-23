using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.Localization.Settings;

//This class is used to calculate player exp. Player exp determines which tools player can unlock.
public class PlayerReputation : MonoBehaviourPun
{
    public float CurrentReputationExp { get; private set; }

    public float IncreaseExpAmount { get; private set; }
    public int CurrentRepLevel { get; private set; }


    [SerializeField] public bool IsXrayLocked { get; private set; }
    [SerializeField] public bool IsVacuumLocked { get; private set; }
    [SerializeField] public bool IsShoeLocked { get; private set; }

    private LocalizationSettings locSettings;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("reputation"))
        {
            CurrentReputationExp = PlayerPrefs.GetFloat("reputation");
        }
        //CurrentReputationExp = 395;//This should come from save/load later on.
        IncreaseExpAmount = 0;
        DetermineReputationLevel();
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        InitLoc();
    }

    private async void InitLoc()
    {
        var handle = LocalizationSettings.InitializationOperation;
        await handle.Task;
        locSettings = handle.Result;
    }

    //Check the rep level of the player. if player has enough rep level, propmt the pop up to inform player about unlocking the new tools.
    private void DetermineReputationLevel()
    {
        CurrentReputationExp += IncreaseExpAmount;
        PlayerPrefs.SetFloat("reputation", CurrentReputationExp);
        //Debug.Log($"Old exp : {CurrentReputationExp} , old Level : {CurrentRepLevel} ");
        int oldLevel = CurrentRepLevel;
        CurrentRepLevel = (int)(CurrentReputationExp / 100);
        if (CurrentRepLevel > oldLevel)
        {
            bool oldVacuum = IsVacuumLocked;
            bool oldShoe = IsShoeLocked;
            bool oldXray = IsXrayLocked;

            DetermineToolProgression();

            Debug.Log($"old locked {oldShoe}, new lock {IsShoeLocked}");
            if (oldShoe && !IsShoeLocked)
            {
                switch (locSettings.GetSelectedLocale().Identifier.Code.ToString().ToLower())
                {
                    case "nl":
                        UIManager.Instance.ShowPopUp("Gefeliciteerd!! Je hebt hardloopschoenen ontgrendeld! Nu kun je een tijdje sneller rennen!!");
                        break;
                    case "ro":
                        UIManager.Instance.ShowPopUp("Felicit?ri!! Ai deblocat pantofi de alergare! Acum po?i s? alergi mai repede pentru o perioad? de timp!!");
                        break;
                    default:
                        UIManager.Instance.ShowPopUp("Congratulations!! You have unlocked running shoes! Now you are able to run faster for a period of time!!");
                        break;
                }
            }
            else if (oldVacuum && !IsVacuumLocked)
            {
                switch (locSettings.GetSelectedLocale().Identifier.Code.ToString().ToLower())
                {
                    case "nl":
                        UIManager.Instance.ShowPopUp("Gefeliciteerd!! Je hebt Stofzuiger ontgrendeld! Nu kunt u afval sneller en gemakkelijker ophalen!!");
                        break;
                    case "ro":
                        UIManager.Instance.ShowPopUp("Felicit?ri!! Ai deblocat Aspiratorul! Acum po?i ridica gunoiul mai repede ?i mai u?or!!");
                        break;
                    default:
                        UIManager.Instance.ShowPopUp("Congratulations!! You have unlocked Vacuum cleaner! Now you are able to pickup trash faster and easier!!");
                        break;
                }
            }
            else if (oldXray && !IsXrayLocked)
            {
                switch (locSettings.GetSelectedLocale().Identifier.Code.ToString().ToLower())
                {
                    case "nl":
                        UIManager.Instance.ShowPopUp("Gefeliciteerd!! Je hebt de X Ray-bril ontgrendeld. Nu kun je alle spellen zien die je om je heen kunt spelen!!");
                        break;
                    case "ro":
                        UIManager.Instance.ShowPopUp("Felicit?ri!! Ai deblocat ochelarii cu raze X. Acum po?i vedea toate jocurile pe care le po?i juca în jurul t?u!!");
                        break;
                    default:
                        UIManager.Instance.ShowPopUp("Congratulations!! You have unlocked the X Ray goggles. Now you are able to see all the games you can play around you!!");
                        break;
                }
            }
        }
        //Debug.Log($"Current exp : {CurrentReputationExp} , Current Level : {CurrentRepLevel} ");
        IncreaseExpAmount = 0;
        DetermineToolProgression();
    }

    //Only use the rep popups on office for coherency.

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "NewOffice")
        {
            DetermineReputationLevel();
        }
    }

    //Simple switch case to see what is locked and what isnt per reputation level.
    private void DetermineToolProgression()
    {
        switch (CurrentRepLevel)
        {
            case 1:
                IsShoeLocked = true;
                IsVacuumLocked = true;
                IsXrayLocked = true;
                break;

            case 2:
                IsShoeLocked = false;
                IsVacuumLocked = true;
                IsXrayLocked = true;

                break;
            case 3:
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = true;
                break;
            case 4:
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = false;
                break;
            default:
                //After lv 5 everything is open/ I know case 4 can be deleted but for now I'll keep it in case we put something else.
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = false;
                break;
        }
    }


    //This is used for increasing the exp for each players reputation level. every 100 points is 1 level.
    public void IncreaseEXP(int remainingSeconds, int nrOfHardGames, int nrOfMediumGames, int nrOfEasyGames, bool dayFailed)
    {
        float increaseAmount = 5f;
        if (!dayFailed)
        {
            if (remainingSeconds > 60)
            {
                increaseAmount += 10f;
            }
            else
            {
                increaseAmount += (float)remainingSeconds / 300f;
            }
            //Debug.Log($"Easy buff: {(float)nrOfEasyGames}");
            increaseAmount += (float)nrOfEasyGames;
            //Debug.Log($"Medium buff: {(float)nrOfMediumGames*2}");
            increaseAmount += (float)nrOfMediumGames * 2;
            //Debug.Log($"Hard buff: {(float)nrOfHardGames * 3f}");
            increaseAmount += (float)nrOfHardGames * 3f;
        }
        //Debug.Log($"Before adjustment of 100, the amount is: {increaseAmount}");

        if (increaseAmount > 100f)
        {
            increaseAmount = 32f;
        }
        //Debug.Log($"final amount: {increaseAmount}");
        IncreaseExpAmount = increaseAmount;
    }
}
