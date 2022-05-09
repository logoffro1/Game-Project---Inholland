using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RecycleMiniGame : MiniGameBase
{
    private int lifes = 3;

    private int amountToCollect = 5;
    private int amountCollected = 0;

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

    private NoteSpawner noteSpawner;
    private bool currentlyLoosing;

    private void Start()
    {
        audioSource = GetComponents<AudioSource>();
        ui = GetComponent<RecycleUI>();
        ui.SetUp(amountToCollect);
        SetLocalizedString();
    }
    public override void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }

    public override void CoordinateLevel()
    {
        //Amount to collect
        int minAmountToCollect = 2;
        int maxAmountToCollect = 20;
        //Speed
        float minSpeed = 0.08f;
        float maxSpeed = 0.3f;
        //Min wait time
        float minMinWaitTime = 0.6f;
        float maxMinWaitTime = 1.4f;
        //Max wait time
        float minMaxWaitTime = 1.4f;
        float maxMaxWaitTime = 3.6f;

        noteSpawner = GetComponentInChildren<NoteSpawner>();
        amountToCollect = minAmountToCollect + (int)(Mathf.CeilToInt(maxAmountToCollect - minAmountToCollect) * (Level / 100));
        noteSpawner.Speed = minSpeed + (Mathf.Ceil(maxSpeed - minSpeed) * (Level / 100));
        noteSpawner.MinWaitTime = minMinWaitTime + (Mathf.Ceil(maxMinWaitTime - minMinWaitTime) * ((100 - Level) / 100));
        noteSpawner.MaxWaitTime = minMaxWaitTime + (Mathf.Ceil(maxMaxWaitTime - minMaxWaitTime) * ((100 - Level) / 100));

    }

    public void RemoveALife()
    {
        if (!gameIsWon && !currentlyLoosing)
        {
            currentlyLoosing = true;

            lifes--;
            ui.RemoveAHeart(lifes);
            activator.RemoveFirstNote();
            audioSource[0].PlayOneShot(BadNote);

            currentlyLoosing = false;

            if (lifes <= 0) GameFinish(false);
        }
    }

    public void CollectANote(NoteTypeEnum note)
    {
        amountCollected++;
        ui.AddToCounter(amountCollected);
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

        if (amountCollected >= amountToCollect)
        {
            gameIsWon = true;
            GameFinish(true);
        }
    }
}
