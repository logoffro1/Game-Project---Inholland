using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//ADD NEW UPGRADE TO SWITCH CASE OF CONSTRUCTOR, AND ADD CORREDONPOSING METHOD
public class OneOffUpgrade
{
    private OneOffUpgradeContent[] contentList;
    private OneOffUpgradesEnum upgrade;
    public OneOffUpgradesEnum Upgrade { get { return upgrade; } private set { upgrade = value; } }

    private string title;
    public string Title { get { return title; } private set { title = value; } }
    private string description;
    public string Description { get { return description; } private set { description = value; } }

    private int level;
    public int Level { get { return level; } private set { level = value; } }

    private Action levelUpFunction;
    private int maxLevel = 8;
    public int MaxLevel { get { return maxLevel; } private set { maxLevel = value; } }

    //For optimizing purposed
    private Player player;
    private PlayerMovement playerMovement;
    private MiniGameBase miniGameBase;


    //fields
    public float LevelOffSet { get; set; }
    public float PointsOffSet { get; set; }
    public int TimeAddAfterMiniGame { get; set; }

    public OneOffUpgrade() 
    {
        level = 0;
    }

    public OneOffUpgrade(OneOffUpgradesEnum upgrade, OneOffUpgradeContent[] oneOffUpgradeContentArray,Player player, PlayerMovement playerMovement, MiniGameBase miniGameBase) 
        :this(upgrade, oneOffUpgradeContentArray)
    {
        this.player = player;
        this.playerMovement = playerMovement;
        this.miniGameBase = miniGameBase;
    }


    public OneOffUpgrade(OneOffUpgradesEnum upgrade, OneOffUpgradeContent[] oneOffUpgradeContentArray)
    {
        //Sets all the content of the upgrades
        this.contentList = oneOffUpgradeContentArray;
        this.upgrade = upgrade;

        //sets to the default
        level = 0;
        LevelOffSet = 0f;
        PointsOffSet = 0f;
        OneOffUpgradeContent[] array = null;

        //Gets the correct text for each type of upgrade
        switch (upgrade)
        {
            case OneOffUpgradesEnum.Speed:
                levelUpFunction = SpeedLevelUp;
                array = contentList.Where(x => x.Upgrade == OneOffUpgradesEnum.Speed).ToArray();
                break;
            case OneOffUpgradesEnum.MinigameDifficultyDecrease:
                levelUpFunction = MinigameDifficultyDecreaseLevelUp;
                array = contentList.Where(x => x.Upgrade == OneOffUpgradesEnum.MinigameDifficultyDecrease).ToArray();
                break;
            case OneOffUpgradesEnum.MinigamePointsIncrease:
                levelUpFunction = MinigamePointsIncreaseLevelUp;
                array = contentList.Where(x => x.Upgrade == OneOffUpgradesEnum.MinigamePointsIncrease).ToArray();
                break;
            case OneOffUpgradesEnum.AddedTimeAfterMinigame:
                levelUpFunction = AddedTimeAfterMinigameLevelUp;
                array = contentList.Where(x => x.Upgrade == OneOffUpgradesEnum.AddedTimeAfterMinigame).ToArray();
                break;
            default:
                break;
        }
        title = array[0].isTitle ? array[0].gameObject.GetComponent<Text>().text : array[1].gameObject.GetComponent<Text>().text;
        description = array[0].isTitle ? array[1].gameObject.GetComponent<Text>().text : array[0].gameObject.GetComponent<Text>().text;
    }

    //Levels up the upgrade
    public void PerformLevelUp()
    {
        this.levelUpFunction();
        level++;
    }

    //Below is the logic to how each upgrade is levels up

    public void SpeedLevelUp()
    {
        float amountIncrement = 0.5f;
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

    public void AddedTimeAfterMinigameLevelUp()
    {
        int amountIncrement = 2;
        TimeAddAfterMiniGame += amountIncrement;
    }

}

//Each uypgrade as anenum
public enum OneOffUpgradesEnum
{
    Speed,
    MinigameDifficultyDecrease,
    MinigamePointsIncrease,
    AddedTimeAfterMinigame
}
