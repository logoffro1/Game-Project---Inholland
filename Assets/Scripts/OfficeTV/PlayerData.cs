using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public bool IsCurrentlyInMission;
    public DistrictEnum IsInDistrict;
    public float NewSustainabilityPoints;

    public float OverallAlkmaarSustainability { get; private set; }
    public float CityCenterSustainability { get; private set; }
    public float FarmSustainability { get; private set; }
    public float LastMapSustainability { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //Load data from save file
        //temp
        if (CityCenterSustainability == 0)
        {
            CityCenterSustainability = 0.3f;
            FarmSustainability = 0.6f;
            LastMapSustainability = 0.43f;
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
                break;
            case DistrictEnum.FarmLand:
                SetFarmSustainability(FarmSustainability + value);
                break;
            case DistrictEnum.ThirdMap:
                SetLastMapSustainability(LastMapSustainability + value);
                break;
        }
    }

    private void SetOverallAlkmaarSustainability()
    {
        OverallAlkmaarSustainability = (CityCenterSustainability + FarmSustainability + LastMapSustainability) / 3;
    }
}
