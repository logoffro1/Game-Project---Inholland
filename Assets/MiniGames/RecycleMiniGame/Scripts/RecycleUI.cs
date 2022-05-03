using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecycleUI : MonoBehaviour
{
    public List<GameObject> lives;
    public TextMeshProUGUI ProgressText;
    private int amountToCollect;

    public void SetUp(int amountToCollect)
    {
        this.amountToCollect = amountToCollect;
        ProgressText.text = $"0 / {amountToCollect}";
    }

    public void RemoveAHeart(int lifes)
    {
        if (lifes < 0) return;

        GameObject life = lives[lifes];
        life.GetComponent<Animator>().SetTrigger("CollectiblePassed");
    }

    public void AddToCounter(int amountCollected)
    {
        ProgressText.text = $"{amountCollected} / {amountToCollect}";
    }
}
