using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

//The map selection screen lgic
public class OverallDistrictScreen : MonoBehaviour
{
    //Gets the components of the editor
    public TextMeshProUGUI topSelectText;
    public TextMeshProUGUI middleSelectText;
    public TextMeshProUGUI overallPercentage;
    public PlayerData PlayerData;
    public Gradient mapGradient;

    private void Awake()
    {
        foreach (PlayerData pd in FindObjectsOfType<PlayerData>())
        {
            if (pd.photonView.IsMine)
            {
                PlayerData = pd;
            }
        }
        DontDestroyOnLoad(PlayerData);
    }

    //Sets the percentage text
    private void Start()
    {
        overallPercentage.text = ConvertFloatPercentageToString(PlayerData.OverallAlkmaarSustainability);
    }

    //Sets the string to correct format
    public string ConvertFloatPercentageToString(float percentage)
    {
        return percentage.ToString("0.0")+ "%";
    }
}
