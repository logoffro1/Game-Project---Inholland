using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using Photon.Pun;

//Timer countdown
public class TimerCountdown : MonoBehaviourPun, IPunObservable
{
    //Default amount of seconds
    private int secondsMax = 8 * 60;

    private static TimerCountdown _instance;
    public static TimerCountdown Instance { get { return _instance; } }
    public int SecondsMax
    {
        private set
        {
            secondsMax = value;
        }

        get
        {
            return secondsMax;
        }
    }

    //Singleton-esque as the timer is the same per each mission
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private int secondsLeft;
    public int SecondsLeft
    {
        get { return secondsLeft; }
        set { secondsLeft = value; }
    }

    public int StartCountDownLeft
    {
        get { return startCountDownLeft; }
        private set { startCountDownLeft = value; }
    }

    private int startCountDownLeft = 4;

    //Events
    public event Action<int> OnSecondChange;
    public event Action<string> OnStartCountdownChange;
    public event EventHandler OnCountdownEnd;
    private GameMode gameMode;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(secondsLeft);
            stream.SendNext(startCountDownLeft);
        }
        else if (stream.IsReading)
        {
            secondsLeft = (int)stream.ReceiveNext();
            OnSecondChange?.Invoke(secondsLeft);

            startCountDownLeft = (int)stream.ReceiveNext();
            OnStartCountdownChange?.Invoke(startCountDownLeft.ToString());
        }
    }
    void Start()
    {
        secondsLeft = secondsMax;

        //Checking the gamemode & multiplayer
        MiniGameManager.Instance.FreezeScreen(true);
        foreach (PlayerData pd in FindObjectsOfType<PlayerData>())
        {
            if (pd.photonView.IsMine)
            {
                gameMode = pd.IsInGameMode;
            }
        }

        //starting the actual countdown
        StartCoroutine(StartCountDown());

    }
    private IEnumerator StartCountDown()
    {
        //The starting "3, 2, 1, go" countdown, before the official countdown
        while (startCountDownLeft > 0)
        {
            yield return new WaitForSeconds(1);
            startCountDownLeft -= 1;

            if (startCountDownLeft <= 0)
            {
                OnStartCountdownChange?.Invoke("Go!");

                yield return new WaitForSeconds(1);

                //Starts the official countdown
                MiniGameManager.Instance.FreezeScreen(false);
                StartCoroutine(TimerTake());
                yield break;
            }
            else if (startCountDownLeft <= 3)
            {
                OnStartCountdownChange?.Invoke(startCountDownLeft.ToString());
            }
        }
    }

    //The actual countdown in the mission
    private IEnumerator TimerTake()
    {
        while (secondsLeft > 0)
        {
            //If the sustainaility bar is at 100%, it should end teh countdown, which will end the mission
            if (ProgressBar.Instance.GetSlideValue() == ProgressBar.Instance.GetSliderMaxValue())
            {
                break;
            }

            //Waits a second
            yield return new WaitForSeconds(1);
            
            //If the gamemode is not chill, it should subtract a second. Else, it doesn;t, as chill mode doesnt have a timer
            if (PhotonNetwork.IsMasterClient)
                if (gameMode != GameMode.Chill) secondsLeft -= 1;
            //Changes the UI
            OnSecondChange?.Invoke(secondsLeft);

            VisualPollution.Instance.UpdateVisualPollution(ProgressBar.Instance.GetSlideValue());
        }

        //Ends the counter
        OnCountdownEnd?.Invoke(this, EventArgs.Empty);
    }
    public int GetRemainingTime()
    {
        return secondsLeft;
    }
}
