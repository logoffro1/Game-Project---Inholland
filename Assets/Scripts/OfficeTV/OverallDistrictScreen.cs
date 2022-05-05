using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OverallDistrictScreen : MonoBehaviour
{
    public TextMeshProUGUI topSelectText;
    public TextMeshProUGUI middleSelectText;
    public TextMeshProUGUI overallPercentage;
    public PlayerData PlayerData;
    public Gradient mapGradient;

    private void Awake()
    {
        PlayerData = FindObjectOfType<PlayerData>();
        DontDestroyOnLoad(PlayerData);
    }

    private void Start()
    {
        overallPercentage.text = ConvertFloatPercentageToString(PlayerData.OverallAlkmaarSustainability);
    }

    public string ConvertFloatPercentageToString(float percentage)
    {
        return string.Format("{0:0}%", percentage * 100);
    }

}
