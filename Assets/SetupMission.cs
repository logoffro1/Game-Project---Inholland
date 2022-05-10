using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class SetupMission : MonoBehaviour
{
    private GameMode gameMode;
    private Player player;

    public TextMeshProUGUI goalText;
    public GameObject timer;


    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = FindObjectOfType<PlayerData>();
        player = FindObjectOfType<Player>();
        gameMode = data.IsInGameMode;
        goalText.text = data.GoalText;

        StartCoroutine(SetGameMode());
    }

    private IEnumerator SetGameMode()
    {
        yield return new WaitForEndOfFrame();
        switch (gameMode)
        {
            case GameMode.Chill:
                TimerCountdown.Instance.SecondsLeft = (60 * 60 * 2); //2 hours
                timer.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
