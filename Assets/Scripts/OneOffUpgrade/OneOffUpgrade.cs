using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//ADD NEW UPGRADE TO SWITCH CASE OF CONSTRUCTOR, AND ADD CORREDONPOSING METHOD
public class OneOffUpgrade : MonoBehaviour
{
    private OneOffUpgradesEnum upgrade;
    public OneOffUpgradesEnum Upgrade { get { return upgrade; } private set { upgrade = value; } }

    private string title;
    public string Title { get { return title; } private set { title = value; } }
    private string description;
    public string Description { get { return description; } private set { description = value; } }

    private int level;
    public int Level { get { return level; } private set { level = value; } }

    private Action levelUpFunction;
    private int maxLevel = 10;
    public int MaxLevel { get { return maxLevel; } private set { maxLevel = value; } }

    //For optimizing purposed
    private Player player;
    private PlayerMovement playerMovement;
    private MiniGameBase miniGameBase;


    //fields
    public float LevelOffSet { get; set; }
    public float PointsOffSet { get; set; }

    private void Start()
    {
        UpgradeManager manager = FindObjectOfType<UpgradeManager>();
        player = manager.Player;

        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        miniGameBase = FindObjectOfType<MiniGameBase>();

        LevelOffSet = 0f;
        PointsOffSet = 0f;
    }

    public OneOffUpgrade() 
    {
        level = 0;
    }


    public OneOffUpgrade(OneOffUpgradesEnum upgrade)
    {
        this.upgrade = upgrade;
        level = 0;

        //ADD NEW UPGRADE TO SWITCH CASE
        switch(upgrade)
        {
            case OneOffUpgradesEnum.Speed:
                levelUpFunction = SpeedLevelUp;
                title = "Walking speed increase";
                description = "You get to walk faster, maybe even up to a sprint?!";
                break;
            case OneOffUpgradesEnum.MinigameDifficultyDecrease:
                levelUpFunction = MinigameDifficultyDecreaseLevelUp;
                title = "Minigames' difficulty level decrease";
                description = "All minigames will get easier, and you'll earn the same amount of points!";
                break;
            case OneOffUpgradesEnum.MinigamePointsIncrease:
                levelUpFunction = MinigamePointsIncreaseLevelUp;
                title = "Minigames' points increase";
                description = "Get more points when you successfully complete a minigame!";
                break;
                /*
            case OneOffUpgradesEnum.AddLotsToTimer:
                levelUpFunction = SpeedLevelUp;
                title = "AddLotsToTimer";
                description = "?!";
                break;
            case OneOffUpgradesEnum.AddEachMiniGameSuccessTimer:
                levelUpFunction = SpeedLevelUp;
                title = "AddEachMiniGameSuccessTimer";
                description = "?!";
                break;
            case OneOffUpgradesEnum.ExtraLifeInMiniGame:
                levelUpFunction = SpeedLevelUp;
                title = "ExtraLifeInMiniGame";
                description = "?!";
                break;
                */
            default:
                break;
        }
    }

    public void PerformLevelUp()
    {
        this.levelUpFunction();
        level++;
    }

    public void SpeedLevelUp()
    {
        float amountIncrement = 2f;
        if (playerMovement == null) playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.Speed += amountIncrement;
    }


    public void MinigameDifficultyDecreaseLevelUp()
    {
        float amountIncrement = -2f;
        LevelOffSet += amountIncrement;
    }

    public void MinigamePointsIncreaseLevelUp()
    {
        float amountIncrement = 1f;
        PointsOffSet += amountIncrement;
    }

    public static List<OneOffUpgrade> SetUpList()
    {
        List<OneOffUpgrade> list = new List<OneOffUpgrade>();
        foreach (OneOffUpgradesEnum upgrade in (OneOffUpgradesEnum[])Enum.GetValues(typeof(OneOffUpgradesEnum)))
        {
            list.Add(new OneOffUpgrade(upgrade));
        }

        return list;
    }



}

public enum OneOffUpgradesEnum
{
    Speed,
    MinigameDifficultyDecrease,
    MinigamePointsIncrease
}
