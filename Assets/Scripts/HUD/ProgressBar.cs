using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public Gradient gradient;
    public Image fill;


    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = 100f;
        slider.minValue = 0f;
        slider.value =5f;
        fill.color = gradient.Evaluate(0.1f);
    }

    private void Update()
    {
        //This should be deleted. Only for testing purposes.
        if (Input.GetKeyDown(KeyCode.Z))
            IncreaseSustainibility(5f);
        if (Input.GetKeyDown(KeyCode.X))
            DecreaseSustainibility(5f);
       
    }

    public void DecreaseSustainibility (float pollutionChange)
    {
        if (slider.value + pollutionChange > 100f)
            slider.value = slider.maxValue;
        else
            slider.value = (slider.value + pollutionChange);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void IncreaseSustainibility (float pollutionChange)
    {
        if(slider.value-pollutionChange < 0)
            slider.value = slider.minValue;
        else
        slider.value = (slider.value - pollutionChange);

        fill.color = gradient.Evaluate(slider.normalizedValue);


    }

    
}
