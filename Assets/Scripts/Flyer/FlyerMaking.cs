using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlyerMaking : MonoBehaviour
{
    //The containers
    public GameObject AllTitles;
    public GameObject AllBodies;

    //Buttons
    public GameObject[] TitleButtons;
    public GameObject[] BodyButtons;

    //Text components
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Body;

    //Flyer counters
    public TextMeshProUGUI Counter;
    public TextMeshProUGUI AmountPrinted;
    private int AmountToPrintPerFlyer = 5;

    [HideInInspector] public float TotalPoints;
    [HideInInspector] public int AmountAccessed;
    [HideInInspector] public int AmountFlyersMade = 0;
    [HideInInspector] public int AmountFlyersToMake = 3;

    //Flyers that were maine
    public List<Flyer> flyerList;
    [HideInInspector] public float PointForCurrentFlyer;

    //Privaelist
    private List<GameObject> titleList;
    private List<GameObject> bodyList;

    [HideInInspector] public bool IsDoneForToday = false;
    [HideInInspector] public Printer Printer;
    private GameObject player;
    private FlyerBag playerFlyerBag;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player"); //change for MP
        playerFlyerBag = player.GetComponent<FlyerBag>();
        if (!playerFlyerBag.CanCollect()) return;

        //Sets counter up for the begining
        SetCounter();
        flyerList = new List<Flyer>();

        //Sets all the strings of titles from the containers
        titleList = new List<GameObject>();
        foreach (Transform child in AllTitles.transform)
        {
            titleList.Add(child.gameObject);
        }

        //Sets all the strings of bodies  from the containers
        bodyList = new List<GameObject>();
        foreach (Transform child in AllBodies.transform)
        {
            bodyList.Add(child.gameObject);
        }

        SettingNewValues();
    }
    private void SettingNewValues()
    {
        //Sets a random text to each button in the flyer for title
        foreach (GameObject titleButton in TitleButtons)
        {
            int index = Random.Range(0, titleList.Count);
            FlyerButton flyerButton = titleButton.GetComponent<FlyerButton>();
            flyerButton.SetButton(titleList[index].GetComponent<TextMeshProUGUI>().text, titleList[index].GetComponent<FlyerOption>().Points);
            titleList.RemoveAt(index);
        }

        //Sets a random text to each button in the flyer for body
        foreach (GameObject bodyButton in BodyButtons)
        {
            int index = Random.Range(0, bodyList.Count);
            FlyerButton flyerButton = bodyButton.GetComponent<FlyerButton>();
            flyerButton.SetButton(bodyList[index].GetComponent<TextMeshProUGUI>().text, bodyList[index].GetComponent<FlyerOption>().Points);
            bodyList.RemoveAt(index);
        }
    }

    //Sets the points
    public void PressedAButton(float amountNewPoints)
    {
        TotalPoints += amountNewPoints;
        AmountAccessed++;
    }

    public void MadeNewFlyer()
    {
        //Adds flyer to list
        Flyer flyer = new Flyer(Title.text, Body.text, PointForCurrentFlyer / 2, AmountToPrintPerFlyer);
        flyerList.Add(flyer);
        for(int i = 0; i < flyer.AmountToPrint; i++)
        {
            playerFlyerBag.AddFlyer(new Flyer(flyer.Title, flyer.Body, flyer.Points, 1));
        }

        //Leaves a bit of time for player can see flyer
        StartCoroutine(ViewTheFlyer());
    }

    private void SetCounter()
    {
        Counter.text = $"{AmountFlyersMade} / {AmountFlyersToMake}";
    }

    private IEnumerator ViewTheFlyer()
    {
        AmountFlyersMade++;
        SetCounter();

        yield return new WaitForSeconds(3f);

        //Checks if amount to make flyers is reaches
        if (AmountFlyersMade >= AmountFlyersToMake)
        {
            //Sets values to show that all flyers are finished
            this.Printer.ProgressPanel.SetActive(true);
            AmountPrinted.text = (AmountFlyersToMake * AmountToPrintPerFlyer).ToString();
            IsDoneForToday = true;
            this.Printer.PrinterPanel.SetActive(false);

            yield return new WaitForSeconds(2f);

            //Return to the office
            Printer.ReturnToOpenWorld(); 
            yield return null;
        }

        //Reset everything
        PointForCurrentFlyer = 0;
        SettingNewValues();
        EnableButtons(TitleButtons, true);
        EnableButtons(BodyButtons, true);
        Title.text = "";
        Body.text = "";
        AmountAccessed = 0;
    }

    public void EnableButtons(GameObject[] buttons, bool wantToEnable)
    {
        foreach (GameObject button in buttons)
        {
            UnityEngine.UI.Button myButton = button.GetComponent<UnityEngine.UI.Button>();
            myButton.interactable = wantToEnable;
        }
    }

}
