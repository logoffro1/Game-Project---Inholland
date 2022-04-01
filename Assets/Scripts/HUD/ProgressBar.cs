using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{

    [SerializeField] private Text _SliderText;

    private Slider slider;
    public Gradient gradient;
    public Image fill;
    private float sliderThreshhold;


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
        sliderThreshhold = 40f;
        slider.value = sliderThreshhold;
        fill.color = gradient.Evaluate(0.1f);
        _SliderText.text = slider.value.ToString("0.00") + "%";
    }

    private void Update()
    {
        decreaseSustainibilityPerSecond(-0.0005f);
    }

    private void updateProgressPercent()
    {
        slider.onValueChanged.AddListener((v) => {
            _SliderText.text = v.ToString("0.00") + "%";
        });
    }

    private void decreaseSustainibilityPerSecond(float sustainibilityValue)
    {
        if (slider.value > sliderThreshhold)
        {
            slider.value += sustainibilityValue;
            fill.color = gradient.Evaluate(slider.normalizedValue);

        }
        else
        {
            return;
        }

    }

    private IEnumerator ApplySliderAnimation(float target)
    {
        yield return new WaitForSeconds(2);

        float current = slider.value;
        float t = 0.0f;
        float elapsedTime = 0.0f;
        float waitTime = 1f;
        while (elapsedTime<waitTime)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(slider.value, target,elapsedTime/waitTime);
            t += 0.5f * elapsedTime;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return null;
            
            
        }

    }
   
    public void ChangeSustainibility(float sustainabilityChange)
    {
        StartCoroutine(ApplySliderAnimation(slider.value + sustainabilityChange));
        updateProgressPercent();
    }

}
