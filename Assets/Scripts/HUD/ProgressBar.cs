using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public Gradient gradient;
    public Image fill;


    private static ProgressBar _instance;
    public static ProgressBar Instance { get { return _instance; } }


    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        SliderInit();
    }

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

    private void SliderInit()
    {
        slider.maxValue = 100f;
        slider.minValue = 0f;
        slider.value = 5f;
        fill.color = gradient.Evaluate(0.1f);
    }

    private void Update()
    {
        //This should be deleted. Only for testing purposes.
        if (Input.GetKeyDown(KeyCode.Z))
            ChangeSustainibility(5f);
        if (Input.GetKeyDown(KeyCode.X))
            ChangeSustainibility(-5f);
       
    }

    public void ChangeSustainibility (float sustainabilityChange)
    {
        slider.value += sustainabilityChange;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
   
}
