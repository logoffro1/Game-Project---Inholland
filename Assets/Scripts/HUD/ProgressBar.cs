using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Slider slider;


    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = 100f;
        slider.minValue = 0f;
        slider.value = 75f;
    }

    private void Update()
    {
        //This should be deleted. Only for testing purposes.
        if (Input.GetKeyDown(KeyCode.Z))
            DecreasePollution(5f);
        if (Input.GetKeyDown(KeyCode.X))
            IncreasePollution(5f);
    }

    public void IncreasePollution (float pollutionChange)
    {
        if (slider.value + pollutionChange > 100f)
            slider.value = slider.maxValue;
        else
            slider.value = (slider.value + pollutionChange);
    }

    public void DecreasePollution (float pollutionChange)
    {
        if(slider.value-pollutionChange < 0)
            slider.value = slider.minValue;
        else
        slider.value = (slider.value - pollutionChange);
    }

    
}
