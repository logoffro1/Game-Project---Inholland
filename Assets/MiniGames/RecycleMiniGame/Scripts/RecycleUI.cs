using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecycleUI : MonoBehaviour
{
    public List<GameObject> lives;
    public TextMeshProUGUI ProgressText;
    private int amountToCollect;
    public GameObject Explosion;

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

    public void ExplodeParticles(GameObject note)
    {
        GameObject explosion = Instantiate(Explosion, note.transform.position, note.transform.rotation, note.transform);
        var main = explosion.GetComponent<ParticleSystem>().main;
        main.startColor = note.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
    }
}
