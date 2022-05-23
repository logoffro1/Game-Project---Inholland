using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //Canvas
    private UpgradeUI upgradeCanvas;
    private bool canvasIsOn;

    //occurences
    private float occurenceAmount = 250f;
    private float amountTilNextOccurence;
    private float amountOfLastOccurence;

    //player
    private Player player;
    public Player Player { get { return player; } }

    void Start()
    {
        //Sets the event
        TimerCountdown.Instance.OnSecondChange += TimerCountdown_OnSecondChange;

        player = FindObjectsOfType<Player>().Where(x => x.Host).FirstOrDefault();
        upgradeCanvas = GetComponent<UpgradeUI>();

        canvasIsOn = false;
        amountTilNextOccurence = ProgressBar.Instance.GetSlideValue() + occurenceAmount;
    }

    //Is called when timer goes down
    private void TimerCountdown_OnSecondChange(int countDown)
    {
        //Gets the values
        int secondsPassed = TimerCountdown.Instance.SecondsMax - countDown;
        float sustainValue = ProgressBar.Instance.GetSlideValue();
        MiniGameManager manager = FindObjectOfType<MiniGameManager>();

        //If condition is met, show the upgrade screen
        if (secondsPassed + sustainValue > amountTilNextOccurence && !canvasIsOn && !manager.IsPlaying)
        {
            canvasIsOn = true;
            amountOfLastOccurence = secondsPassed + sustainValue;
            amountTilNextOccurence = amountOfLastOccurence + occurenceAmount;
            upgradeCanvas.TurnOn();
        }
    }

    public void UpgradeSessionFInished()
    {
        int secondsPassed = TimerCountdown.Instance.SecondsMax - TimerCountdown.Instance.SecondsLeft;
        float sustainValue = ProgressBar.Instance.GetSlideValue();

        canvasIsOn = false;
        amountOfLastOccurence = secondsPassed + sustainValue;
        amountTilNextOccurence = amountOfLastOccurence + occurenceAmount;
    }
}
