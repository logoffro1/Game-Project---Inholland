/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCalculator : MonoBehaviour
{

    //I dont know where to get these from so for now I cap them to 70f for calculation purposes
    public float farmSustainibility = 70f;
    public float cityCenterSustainibility = 70f;
    public float thirdMapSustainibility = 70f;


    private PlayerData currentPlayerData;

    //15 hours of gameplay is the max time - If player is experienced and does the missions well - 7,5-10 hours
    //60/15 900 minutes is 15 hours of gameplay
    //900/8 112 iterations is the required amount of play times for all maps(maximum amount of times)
    //112/3 is 37-38 is the rough number required for every map to be completed
    //2,7f is the minimum amount of sustainibility points the player should receive per mission 
    // 5f is the maximum amount to receive to finish the game faster



    //Possible multipliers :  game difficulty, win condition, extra time.
    private float increaseAmount;

    public float totalAlkmaarSustainibility;
    void Start()
    {
        calculateTotalAlkmaarSustainibility();
        currentPlayerData = FindObjectOfType<PlayerData>();
    }

    private void calculateTotalAlkmaarSustainibility()
    {
        totalAlkmaarSustainibility= (float)((farmSustainibility + cityCenterSustainibility + thirdMapSustainibility) / 3f);
    }

    private float increaseMapSustainibility(int remainingSeconds,int nrOfHardGames, int nrOfMediumGames,int nrOfEasyGames, bool ifWon) {

         increaseAmount = 2.7f;
        if (ifWon) {
            Debug.Log($"Remaining seconds: {(float)remainingSeconds / 30f}");
            increaseAmount += (float)remainingSeconds / 60f;
            Debug.Log($"Easy buff: {(float)nrOfEasyGames / 20f}");
            increaseAmount += (float)nrOfEasyGames / 20f;
            Debug.Log($"Medium buff: {(float)nrOfMediumGames / 10f}");
            increaseAmount += (float)nrOfMediumGames / 10f;
            Debug.Log($"Hard buff: {(float)nrOfHardGames / 5f}");
            increaseAmount += (float)nrOfHardGames / 5f;
        }
        Debug.Log($"Before adjustment of 5, the amount is: {increaseAmount}");

        if (increaseAmount > 5f)
        {
            increaseAmount = 5f;
        }
        Debug.Log($"final amount: {increaseAmount}");
        return increaseAmount;
    }
    // 0 =  city centre,  1 = farm map , 2 = third map
    public void increaseMapSustainibility(int mapIndex,int remainingSeconds, int nrOfHardGames, int nrOfMediumGames, int nrOfEasyGames, bool ifWon) {
        float amountToAdd = increaseMapSustainibility(remainingSeconds, nrOfHardGames, nrOfMediumGames, nrOfEasyGames, ifWon);

        switch (mapIndex)
        {
            case 0:
                if (cityCenterSustainibility + amountToAdd > 100f) {
                    cityCenterSustainibility = 100f;
                }
                else
                {
                    this.cityCenterSustainibility += amountToAdd;
                }
                break;
            case 1:
                if (farmSustainibility + amountToAdd > 100f)
                {
                    farmSustainibility = 100f;
                }
                else
                {
                    this.farmSustainibility += amountToAdd;
                }
                break;

            case 2:
                if (thirdMapSustainibility + amountToAdd > 100f)
                {
                    thirdMapSustainibility = 100f;
                }
                else
                {
                    this.thirdMapSustainibility += amountToAdd;
                }
                break;
        }
        calculateTotalAlkmaarSustainibility();
    } 
}
*/