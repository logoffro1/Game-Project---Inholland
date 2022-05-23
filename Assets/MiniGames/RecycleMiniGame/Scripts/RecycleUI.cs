using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//UI of recycyle game
public class RecycleUI : MonoBehaviour
{
    public List<GameObject> lives;
    public TextMeshProUGUI ProgressText;
    private int amountToCollect;
    public GameObject Explosion;

    public void SetUp(int amountToCollect)
    {
        //Starts it up
        this.amountToCollect = amountToCollect;
        ProgressText.text = $"0 / {amountToCollect}";
    }

    public void RemoveAHeart(int lifes)
    {
        if (lifes < 0) return;

        //Shows animation of losing a heart
        GameObject life = lives[lifes];
        life.GetComponent<Animator>().SetTrigger("CollectiblePassed");
    }

    public void AddToCounter(int amountCollected)
    {
        //Sets the correct string format
        ProgressText.text = $"{amountCollected} / {amountToCollect}";
    }

    public void ConvertToWonDesign(GameObject note)
    {
        //shows ui elements for feedback of getting trash correct
        ExplodeParticles(note);
        MakeNoteTransparent(note);
        MakeNoteSmaller(note);
    }

    private void ExplodeParticles(GameObject note)
    {
        GameObject explosion = Instantiate(Explosion, note.transform.position, note.transform.rotation, note.transform);
        var main = explosion.GetComponent<ParticleSystem>().main;
        main.startColor = note.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
    }

    private void MakeNoteTransparent(GameObject note)
    {
        Color color = note.GetComponent<SpriteRenderer>().color;
        color.a = 0.5f;
        note.GetComponent<SpriteRenderer>().color = color;
    }

    private void MakeNoteSmaller(GameObject note)
    {
        note.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

}
