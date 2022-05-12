using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlyerMaking : MonoBehaviour
{
    public GameObject AllTitles;
    public GameObject AllBodies;

    public GameObject[] TitleButtons;
    public GameObject[] BodyButtons;

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Body;

    public TextMeshProUGUI Counter;
    public TextMeshProUGUI AmountPrinted;

    public List<Flyer> flyerList;
    [HideInInspector] public float PointForCurrentFlyer;

    [HideInInspector] public float TotalPoints;
    [HideInInspector] public int AmountAccessed;
    [HideInInspector] public int AmountFlyersMade = 0;
    [HideInInspector] public int AmountFlyersToMake = 3;

    private int AmountToPrintPerFlyer = 5;

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
        SetCounter();
        flyerList = new List<Flyer>();

        titleList = new List<GameObject>();
        foreach (Transform child in AllTitles.transform)
        {
            titleList.Add(child.gameObject);
        }

        bodyList = new List<GameObject>();
        foreach (Transform child in AllBodies.transform)
        {
            bodyList.Add(child.gameObject);
        }

        SettingNewValues();
    }
    private void SettingNewValues()
    {
        foreach (GameObject titleButton in TitleButtons)
        {
            int index = Random.Range(0, titleList.Count);
            FlyerButton flyerButton = titleButton.GetComponent<FlyerButton>();
            flyerButton.SetButton(titleList[index].GetComponent<TextMeshProUGUI>().text, titleList[index].GetComponent<FlyerOption>().Points);
            titleList.RemoveAt(index);
        }

        foreach (GameObject bodyButton in BodyButtons)
        {
            int index = Random.Range(0, bodyList.Count);
            FlyerButton flyerButton = bodyButton.GetComponent<FlyerButton>();
            flyerButton.SetButton(bodyList[index].GetComponent<TextMeshProUGUI>().text, bodyList[index].GetComponent<FlyerOption>().Points);
            bodyList.RemoveAt(index);
        }
    }

    public void PressedAButton(float amountNewPoints)
    {
        TotalPoints += amountNewPoints;
        AmountAccessed++;
    }

    public void MadeNewFlyer()
    {
        Flyer flyer = new Flyer(Title.text, Body.text, PointForCurrentFlyer / 2, AmountToPrintPerFlyer);
        flyerList.Add(flyer);
        for(int i = 0; i < flyer.AmountToPrint; i++)
        {
            playerFlyerBag.AddFlyer(new Flyer(flyer.Title, flyer.Body, flyer.Points, 1));
        }
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

        //Check
        if (AmountFlyersMade >= AmountFlyersToMake)
        {
            this.Printer.ProgressPanel.SetActive(true);
            AmountPrinted.text = (AmountFlyersToMake * AmountToPrintPerFlyer).ToString();
            IsDoneForToday = true;
            this.Printer.PrinterPanel.SetActive(false);

            yield return new WaitForSeconds(2f);

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
