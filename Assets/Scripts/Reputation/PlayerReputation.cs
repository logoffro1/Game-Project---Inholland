using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReputation : MonoBehaviour
{
    public float CurrentReputationExp { get; private set; }
     public int CurrentRepLevel { get; private set; }

    public bool IsXrayLocked { get; private set; }
    public bool IsVacuumLocked { get; private set; }
    public bool IsShoeLocked { get; private set; }

    private void Start()
    {
        CurrentReputationExp = 100;//This should come from save/load later on.
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
                break;
            case 3:
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = true;
                break;
/*            case 4:
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = false;
                break;*/
            default:
                IsShoeLocked = false;
                IsVacuumLocked = false;
                IsXrayLocked = true;
                break;

        }
    }

    public void IncreaseEXP(int remainingSeconds, int nrOfHardGames, int nrOfMediumGames, int nrOfEasyGames, bool dayFailed)
    {
        float increaseAmount = 2.7f;
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
            Debug.Log($"Easy buff: {(float)nrOfEasyGames / 10f}");
            increaseAmount += (float)nrOfEasyGames / 10f;
            Debug.Log($"Medium buff: {(float)nrOfMediumGames / 5f}");
            increaseAmount += (float)nrOfMediumGames / 5f;
            Debug.Log($"Hard buff: {(float)nrOfHardGames / 2f}");
            increaseAmount += (float)nrOfHardGames / 2f;
        }
        Debug.Log($"Before adjustment of 5, the amount is: {increaseAmount}");

        if (increaseAmount > 100f)
        {
            increaseAmount = 25f;
        }
        Debug.Log($"final amount: {increaseAmount}");
        CurrentReputationExp+= increaseAmount;
        DetermineReputationLevel();
    }
}
