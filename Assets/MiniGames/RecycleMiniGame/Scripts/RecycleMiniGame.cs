using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RecycleMiniGame : MiniGameBase
{
    private int lifes = 3;

    private int amountToCollect = 1;
    private Dictionary<NoteTypeEnum, int> notesCollected;
    public RecycleUI ui;
    public Activator activator;


    private void Start()
    {
        notesCollected = new Dictionary<NoteTypeEnum, int>();

        foreach (NoteTypeEnum type in System.Enum.GetValues(typeof(NoteTypeEnum)))
        {
            notesCollected.Add(type, 0);
        }

        ui = GetComponent<RecycleUI>();
        SetLocalizedString();
    }
    public override void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }

    public override void CoordinateLevel()
    {
       
    }

    public void RemoveALife()
    {
        lifes--;
        ui.RemoveAHeart();
        activator.RemoveFirstNote();
        if (lifes < 0) GameFinish(false);
    }

    public void CollectANote(NoteTypeEnum note)
    {
        notesCollected[note]++;
        activator.RemoveFirstNote();

        if (notesCollected[note] >= amountToCollect && AreAllNotesCollected())
        {
            GameFinish(true);
        }
    }

    private bool AreAllNotesCollected()
    {
        foreach(int amount in notesCollected.Values)
        {
            if (amount < amountToCollect)
                return false;
        }

        return true;
    }

}
