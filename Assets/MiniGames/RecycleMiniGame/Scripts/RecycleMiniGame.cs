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
    private bool gameIsWon = false;

    public AudioClip GoodNote;
    public AudioClip BadNote;
    public AudioClip PaperNote;
    public AudioClip PlasticNote;
    public AudioClip GlassNote;
    public AudioClip OrganiicNote;
    private AudioSource[] audioSource;


    private void Start()
    {
        notesCollected = new Dictionary<NoteTypeEnum, int>();
        audioSource = GetComponents<AudioSource>();

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
        if (!gameIsWon)
        {
            lifes--;
            ui.RemoveAHeart();
            activator.RemoveFirstNote();
            audioSource[0].PlayOneShot(BadNote);

            if (lifes <= 0) GameFinish(false);
        }
    }

    public void CollectANote(NoteTypeEnum note)
    {
        notesCollected[note]++;
        activator.RemoveFirstNote();
        audioSource[0].PlayOneShot(GoodNote);

        switch(note)
        {
            case NoteTypeEnum.Plastic:
                audioSource[1].PlayOneShot(PlasticNote);
                break;
            case NoteTypeEnum.Glass:
                audioSource[1].PlayOneShot(GlassNote);
                break;
            case NoteTypeEnum.Organic:
                audioSource[1].PlayOneShot(OrganiicNote);
                break;
            case NoteTypeEnum.Paper:
                audioSource[1].PlayOneShot(PaperNote);
                break;
            default:
                break;
        }

        if (notesCollected[note] >= amountToCollect && AreAllNotesCollected())
        {
            gameIsWon = true;
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
