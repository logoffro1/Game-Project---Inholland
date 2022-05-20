using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerData : MonoBehaviourPun
{
    public bool IsCurrentlyInMission;
    public DistrictEnum IsInDistrict;
    public GameMode IsInGameMode;
    public string GoalText;
    public float NewSustainabilityPoints;
    public float FlyerPoints;

    public float OverallAlkmaarSustainability { get; private set; }
    public float CityCenterSustainability { get; private set; }
    public float FarmSustainability { get; private set; }
    public float LastMapSustainability { get; private set; }

    private PlayerReportData pData;

    // Start is called before the first frame update
    void Start()
    {
        
        if (CityCenterSustainability == 0)
        {
            if (PlayerPrefs.HasKey("citySus"))
            {
                CityCenterSustainability = PlayerPrefs.GetFloat("citySus");
            }
            else
            {
                CityCenterSustainability = 1f;
            }
            if (PlayerPrefs.HasKey("farmSus"))
            {
                FarmSustainability = PlayerPrefs.GetFloat("farmSus");
            }
            else
                FarmSustainability = 1f;
            if (PlayerPrefs.HasKey("lastMapSus"))
            {
                LastMapSustainability = PlayerPrefs.GetFloat("lastMapSus");
            }
            else
                LastMapSustainability = 1f;

        }

        OverallAlkmaarSustainability = (CityCenterSustainability + FarmSustainability + LastMapSustainability) / 3;
    }

    public void SetCityCenterSustainability(float value)
    {
        CityCenterSustainability = value;
        PlayerPrefs.SetFloat("citySus", CityCenterSustainability);
        SetOverallAlkmaarSustainability();
    }
    public void SetFarmSustainability(float value)
    {
        FarmSustainability = value;
        PlayerPrefs.SetFloat("farmSus", FarmSustainability);
        SetOverallAlkmaarSustainability();
    }
    public void SetLastMapSustainability(float value)
    {
        LastMapSustainability = value;
        PlayerPrefs.SetFloat("lastMapSus", LastMapSustainability);
        SetOverallAlkmaarSustainability();
    }
  
    public void AddToCurrentDistrict(float value)
    { 
        switch(IsInDistrict)
        {
            case DistrictEnum.CityCenter:
                SetCityCenterSustainability(CityCenterSustainability + value);
                if (PlayerPrefs.GetFloat("citySus") > 100f)
                {
                    PlayerPrefs.SetFloat("citySus", 100f);
                }
                break;
            case DistrictEnum.FarmLand:
                SetFarmSustainability(FarmSustainability + value);
                if (PlayerPrefs.GetFloat("farmSus") > 100f)
                {
                    PlayerPrefs.SetFloat("farmSus", 100f);
                }
                break;
            case DistrictEnum.ThirdMap:
                SetLastMapSustainability(LastMapSustainability + value);
                if (PlayerPrefs.GetFloat("lastMapSus") > 100f)
                {
                    PlayerPrefs.SetFloat("lastMapSus", 100f);
                }
                break;
        }
    }
  

    private void SetOverallAlkmaarSustainability()
    {
        OverallAlkmaarSustainability = (CityCenterSustainability + FarmSustainability + LastMapSustainability) / 3;
    }
}
