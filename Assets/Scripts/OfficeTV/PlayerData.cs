using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
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
        //Load data from save file
        //temp
        pData = FindObjectOfType<PlayerReportData>();
        if (CityCenterSustainability == 0)
        {
            CityCenterSustainability = 1f;
            FarmSustainability = 1f;
            LastMapSustainability = 1f;
        }

        OverallAlkmaarSustainability = (CityCenterSustainability + FarmSustainability + LastMapSustainability) / 3;
    }

    public void SetCityCenterSustainability(float value)
    {
        CityCenterSustainability = value;
        SetOverallAlkmaarSustainability();
    }
    public void SetFarmSustainability(float value)
    {
        FarmSustainability = value;
        SetOverallAlkmaarSustainability();
    }
    public void SetLastMapSustainability(float value)
    {
        LastMapSustainability = value;
        SetOverallAlkmaarSustainability();
    }
  
    public void AddToCurrentDistrict(float value)
    { 
        switch(IsInDistrict)
        {
            case DistrictEnum.CityCenter:
                SetCityCenterSustainability(CityCenterSustainability + value);
                if (CityCenterSustainability > 100f)
                {
                    CityCenterSustainability = 100f;
                }
                break;
            case DistrictEnum.FarmLand:
                SetFarmSustainability(FarmSustainability + value);
                if (FarmSustainability > 100f)
                {
                    FarmSustainability = 100f;
                }
                break;
            case DistrictEnum.ThirdMap:
                SetLastMapSustainability(LastMapSustainability + value);
                if (LastMapSustainability > 100f)
                {
                    LastMapSustainability = 100f;
                }
                break;
        }
    }
  

    private void SetOverallAlkmaarSustainability()
    {
        OverallAlkmaarSustainability = (CityCenterSustainability + FarmSustainability + LastMapSustainability) / 3;
    }
}
