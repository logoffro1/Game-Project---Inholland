using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReputation : MonoBehaviour
{
    public float CurrentReputationExp { get; private set; }

    public float IncreaseExpAmount { get; private set; }
     public int CurrentRepLevel { get; private set; }

   [SerializeField] public bool IsXrayLocked { get; private set; }
    [SerializeField] public bool IsVacuumLocked { get; private set; }
    [SerializeField] public bool IsShoeLocked { get; private set; }

    private void Start()
    {
        Debug.Log("Start runned right here");
        CurrentReputationExp = 395;//This should come from save/load later on.
        IncreaseExpAmount = 0;
        DetermineReputationLevel();
        DontDestroyOnLoad(this.gameObject);
    }

    private void DetermineReputationLevel()
    {
        CurrentReputationExp += IncreaseExpAmount;
        Debug.Log($"Old exp : {CurrentReputationExp} , old Level : {CurrentRepLevel} ");
        int oldLevel = CurrentRepLevel;
        CurrentRepLevel = (int)(CurrentReputationExp / 100);

        if (CurrentRepLevel > oldLevel)
        {
            bool oldVacuum = IsVacuumLocked;
            bool oldShoe = IsShoeLocked;
            bool oldXray = IsXrayLocked;

            DetermineToolProgression();


            Debug.Log($"old locked {oldShoe}, new lock {IsShoeLocked}");
            if (oldShoe&& !IsShoeLocked) {
                UIManager.Instance.ShowPopUp("Congrats!! You have unlocked running shoes! Now you are able to run faster for a period of time!!");
            }else if (oldVacuum && !IsVacuumLocked)
            {
                UIManager.Instance.ShowPopUp("Congrats!! You have unlocked Vacuum cleaner! Now you are able to pickup trash faster and easier!!");
            }
            else if (oldXray && !IsXrayLocked)
            {
                UIManager.Instance.ShowPopUp("Congrats!! You have unlocked the XRay goggles. Now you are able to see all the games you can play around you!!");
            }

        }
        Debug.Log($"Current exp : {CurrentReputationExp} , Current Level : {CurrentRepLevel} ");
        IncreaseExpAmount = 0;
        DetermineToolProgression();
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Office runned right here");
        if (SceneManager.GetActiveScene().name == "NewOffice") {
            DetermineReputationLevel();
        }
    }

    private void DetermineToolProgression()
    {
        switch (CurrentRepLevel) {
            case 1:
                IsShoeLocked = true;
                IsVacuumLocked = true;
                IsXrayLocked = true;
                break;

            case 2:
                IsShoeLocked = false;
                IsVacuumLocked = true;
                IsXrayLocked = true;
                Debug.Log($"{IsShoeLocked} is isshoelocked.");

                break;
            case 3:
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = true;
                Debug.Log($"{IsVacuumLocked} is isvacuumlocked.");
                break;
            case 4:
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = false;
                Debug.Log($"{IsXrayLocked} is isXrayLocked.");
                break;
            default:
                //After lv 5 everything is open/ I know case 4 can be deleted but for now I'll keep it in case we put something else.
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = false;
                Debug.Log($"{IsXrayLocked} is isXrayLocked.");
                break;
        }
    }



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
            Debug.Log($"Easy buff: {(float)nrOfEasyGames}");
            increaseAmount += (float)nrOfEasyGames;
            Debug.Log($"Medium buff: {(float)nrOfMediumGames*2}");
            increaseAmount += (float)nrOfMediumGames * 2;
            Debug.Log($"Hard buff: {(float)nrOfHardGames * 3f}");
            increaseAmount += (float)nrOfHardGames * 3f;
        }
        Debug.Log($"Before adjustment of 100, the amount is: {increaseAmount}");

        if (increaseAmount > 100f)
        {
            increaseAmount = 32f;
        }
        Debug.Log($"final amount: {increaseAmount}");
        IncreaseExpAmount = increaseAmount;
    }
}
