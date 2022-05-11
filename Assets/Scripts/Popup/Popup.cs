using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    [HideInInspector] public TaskObjectType Task;
    public bool isMinigamePopup = true; //else is achievement
    public GameObject design;

    //Content containers
    public GameObject minigameSewage;
    public GameObject minigameRecycle;
    public GameObject minigameSolarPanel;
    public GameObject minigameStreetLamp;
    public GameObject minigameWindTurbine;
    public GameObject minigameTree;

    //What changes
    public TextMeshProUGUI factText;
    public GameObject LeafIcon;
    public GameObject TrophyIcon;

    private AudioSource AudioSource;
    public AudioClip AudioClip;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitAndMove(1f));
    }

    private Text GetRandomTextFrom(GameObject container)
    {
        return container.transform.GetChild(Random.Range(0, container.transform.childCount)).GetComponent<Text>();
    }

    private IEnumerator WaitAndMove(float time)
    {
        //Wait for minigame
        yield return new WaitForSeconds(2.5f);
        //change image

        Text chosenText = null;
        switch (Task)
        {
            case TaskObjectType.Bin:
                chosenText = GetRandomTextFrom(minigameRecycle);
                break;
            case TaskObjectType.StreetLamp:
                chosenText = GetRandomTextFrom(minigameStreetLamp);
                break;
            case TaskObjectType.ManHole:
                chosenText = GetRandomTextFrom(minigameSewage);
                break;
            case TaskObjectType.SolarPanel:
                chosenText = GetRandomTextFrom(minigameSolarPanel);
                break;
            case TaskObjectType.WindTurbine:
                chosenText = GetRandomTextFrom(minigameWindTurbine);
                break;
            case TaskObjectType.Tree:
                chosenText = GetRandomTextFrom(minigameTree);
                break;
        }

        factText.text = chosenText.text;
        LeafIcon.SetActive(isMinigamePopup);
        TrophyIcon.SetActive(!isMinigamePopup);

        //Go down
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + (transform.up * -120);
        float elapsedTime = 0;
        AudioSource.PlayOneShot(AudioClip);

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Wait for player to read
        yield return new WaitForSeconds(6f);

        //Goes back up
        elapsedTime = 0;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(finalPos, startingPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(this);
    }

}
