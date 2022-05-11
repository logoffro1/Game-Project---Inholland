using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReputation : MonoBehaviour
{
    public float CurrentReputationExp { get; private set; }
     public int CurrentRepLevel { get; private set; }

   [SerializeField] public bool IsXrayLocked { get; private set; }
    [SerializeField] public bool IsVacuumLocked { get; private set; }
    [SerializeField] public bool IsShoeLocked { get; private set; }

    private void Start()
    {
        CurrentReputationExp = 195;//This should come from save/load later on.
        DetermineReputationLevel();
        DontDestroyOnLoad(this.gameObject);
    }

    private void DetermineReputationLevel()
    {
        Debug.Log($"Old exp : {CurrentReputationExp} , old Level : {CurrentRepLevel} ");
        CurrentRepLevel = (int)(CurrentReputationExp / 100);
        Debug.Log($"Current exp : {CurrentReputationExp} , Current Level : {CurrentRepLevel} ");

        DetermineToolProgression();
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
        CurrentReputationExp+= increaseAmount;
        DetermineReputationLevel();
    }
}
