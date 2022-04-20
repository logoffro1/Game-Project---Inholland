using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//ADD NEW UPGRADE TO SWITCH CASE OF CONSTRUCTOR, AND ADD CORREDONPOSING METHOD
public class OneOffUpgrade 
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
    private int maxLevel = 2;
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

    public OneOffUpgrade(OneOffUpgradesEnum upgrade, Player player, PlayerMovement playerMovement, MiniGameBase miniGameBase) 
        :this(upgrade)
    {
        this.player = player;
        this.playerMovement = playerMovement;
        this.miniGameBase = miniGameBase;
    }


    public OneOffUpgrade(OneOffUpgradesEnum upgrade)
    {
        this.upgrade = upgrade;
        level = 0;
        LevelOffSet = 0f;
        PointsOffSet = 0f;

        //ADD NEW UPGRADE TO SWITCH CASE
        switch (upgrade)
        {
            case OneOffUpgradesEnum.Speed:
                levelUpFunction = SpeedLevelUp;
                title = "Walking speed increase";
                description = "You get to walk faster, maybe even up to a sprint?!";
                break;
            case OneOffUpgradesEnum.MinigameDifficultyDecrease:
                levelUpFunction = MinigameDifficultyDecreaseLevelUp;
                title = "Minigames' difficulty decrease";
                description = "All minigames will get easier, and you'll earn the same amount of points!";
                break;
            case OneOffUpgradesEnum.MinigamePointsIncrease:
                levelUpFunction = MinigamePointsIncreaseLevelUp;
                title = "Minigames' points increase";
                description = "Get more points when you successfully complete a minigame!";
                break;
            case OneOffUpgradesEnum.AddedTimeAfterMinigame:
                levelUpFunction = AddedTimeAfterMinigameLevelUp;
                title = "Added time after minigame";
                description = "You get some time if you finish a minigame successfully!";
                break;
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

public enum OneOffUpgradesEnum
{
    Speed,
    MinigameDifficultyDecrease,
    MinigamePointsIncrease,
    AddedTimeAfterMinigame
}
