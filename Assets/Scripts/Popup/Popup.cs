using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This is the class that creates popups with the correct content in them
public class Popup : MonoBehaviour
{
    [HideInInspector] public TaskObjectType Task;
    public string InfoText { get; set; } = "";
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
        //Gets crandom text of correct container,for the content
        return container.transform.GetChild(Random.Range(0, container.transform.childCount)).GetComponent<Text>();
    }

    private IEnumerator WaitAndMove(float time)
    {
        //Wait for minigame
        if (InfoText.Equals(""))
            yield return new WaitForSeconds(2.5f);


        //Get a random text from the correct container
        Text chosenText = null;
        if (InfoText.Equals(""))
        {
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
        }
        else
        {
            //Else its not a minigame popup
            isMinigamePopup = false;
            chosenText = gameObject.AddComponent<Text>();
            chosenText.text = InfoText;
            Debug.Log(chosenText.text);
        }


        factText.text = chosenText.text;
        LeafIcon.SetActive(isMinigamePopup);
        TrophyIcon.SetActive(!isMinigamePopup);

        //Go down
        Vector3 startingPos = transform.localPosition;
        Vector3 finalPos = transform.localPosition + (transform.up * -120);
        float elapsedTime = 0;
        AudioSource.PlayOneShot(AudioClip);

        while (elapsedTime < time)
        {
            transform.localPosition = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Wait for player to read
        if (InfoText.Equals(""))
            yield return new WaitForSeconds(6f);
        else
            yield return new WaitForSeconds(3f);

        //Goes back up
        elapsedTime = 0;
        while (elapsedTime < time)
        {
            transform.localPosition = Vector3.Lerp(finalPos, startingPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
