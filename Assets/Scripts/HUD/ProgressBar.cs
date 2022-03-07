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
        slider.value = 100f;
    }

    private void Update()
    {
        //This should be deleted. Only for testing purposes.
        if (Input.GetMouseButton(0))
            DecreasePollution(0.2f);
        if (Input.GetMouseButton(1))
            IncreasePollution(0.2f);
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
