using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class SetupMission : MonoBehaviour
{
    private GameMode gameMode;
    private Player player;

    public TextMeshProUGUI goalText;
    public TextMeshProUGUI timerText;
    public RawImage timerImage;
    public GameObject LineOnProgressBar;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetGameMode());
    }

    private IEnumerator SetGameMode()
    {
        yield return new WaitForEndOfFrame();
        PlayerData data = FindObjectOfType<PlayerData>();
        foreach (PlayerData pd in FindObjectsOfType<PlayerData>())
        {
            if (pd.photonView.IsMine)
            {
                data = pd;
            }
        }
        foreach(Player p in FindObjectsOfType<Player>())
        {
            if (p.photonView.IsMine)
            {
                player = p;
            }
        }
        
        gameMode = data.IsInGameMode;
        goalText.text = data.GoalText;

        switch (gameMode)
        {
            case GameMode.Chill:
                TimerCountdown.Instance.SecondsLeft = (60 * 60 * 2); //2 hours
                timerText.enabled = false;
                timerImage.enabled = false;
                ProgressBar.Instance.AmountDecreasingPerSecond = 0;
                LineOnProgressBar.SetActive(false);
                break;
            case GameMode.Crazy:
                TimerCountdown.Instance.SecondsLeft = (40);
                player.OneOffUpgradeList.Where(x => x.Upgrade == OneOffUpgradesEnum.AddedTimeAfterMinigame).FirstOrDefault().TimeAddAfterMiniGame = 15; //gives ten secs after a minigame
                break;
            default:
                break;
        }
        yield return null;

    }

}
