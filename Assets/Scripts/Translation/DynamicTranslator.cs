using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class DynamicTranslator : MonoBehaviour
{
    private static DynamicTranslator _instance;
    public static DynamicTranslator Instance { get { return _instance; } }

    public string mostAttemptedTask = "test";
    public string bonusTimeRemaining = "test";
    public string winconditionDay = "test";

    private PlayerReportData playerReportData;
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
 

    public void translateEndOfTheDayVariables()
    {
        playerReportData = FindObjectOfType<PlayerReportData>();
        int remainingTime = TimerCountdown.Instance.GetRemainingTime();
        int playNr;
        bonusTimeRemaining = $"{remainingTime} seconden resterend";
        winconditionDay = $"{getWinLoseConditionInDutch().ToString()}";
        Debug.Log(getWinLoseConditionInDutch());
        mostAttemptedTask = $"{returnPrefabTaskNameInDutch(playerReportData.GetTheMostPlayedMiniGameName(out playNr)).ToString()} : {playNr} keer.";
        Debug.Log($"{returnPrefabTaskNameInDutch(playerReportData.GetTheMostPlayedMiniGameName(out playNr)).ToString()} : {playNr} keer.");
    }

    private string returnPrefabTaskNameInDutch(string prefabname)
    {
        string value;
        switch (prefabname)
        {
            case "Clean sewers":
                value = "Schone riolen";
                break;

            case "Rewiring Street lamp":
                value = "Straatlantaarn opnieuw bedraden";
                break;
            case "Setting up solar panel":
                value = "Zonnepaneel opzetten";
                break;
            case "Plant trees":
                value = "Bomen planten";
                break;
            case "Converting street lamp to solar lamp":
                value = "Straatlantaarn ombouwen naar zonnelamp";
                break;
            default:
                value = "niet vragen";
                break;
        }
        return value;
    }

     private string getWinLoseConditionInDutch()
    {
        if (ProgressBar.Instance.GetSlideValue() >= 80)
        {
            return " Dag is succesvol afgesloten.";
        }
        else
        {
            return "Dag is mislukt";
        }
    }

}
