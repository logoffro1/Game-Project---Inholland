using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class OverallDistrictScreen : MonoBehaviour
{
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

    private void Start()
    {
        overallPercentage.text = ConvertFloatPercentageToString(PlayerData.OverallAlkmaarSustainability);
    }

    public string ConvertFloatPercentageToString(float percentage)
    {
        return percentage.ToString("0.0")+ "%";
    }

}
