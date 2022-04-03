using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPollution : MonoBehaviour
{
    private static VisualPollution _instance;
    public static VisualPollution Instance { get { return _instance; } }

    public Gradient pollutionGradient;


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

        StartingVisuals();
    }

    private void StartingVisuals()
    {
        float slideValue = ProgressBar.Instance.GetSlideValue();
        RenderSettings.fogColor = pollutionGradient.Evaluate(slideValue / 100);
        RenderSettings.fogDensity = ((100f - slideValue) / (slideValue * 100));

    }

    public void UpdateVisualPollution(float sustainabilityPercentage)
    {
        //0 == no sustainability, 100 == complete
        if (sustainabilityPercentage >= 100f) sustainabilityPercentage = 99.8f;
        
        RenderSettings.fogColor = pollutionGradient.Evaluate(sustainabilityPercentage/100);
        RenderSettings.fogDensity = ((100f - sustainabilityPercentage) / (sustainabilityPercentage * 30));
    }
}
