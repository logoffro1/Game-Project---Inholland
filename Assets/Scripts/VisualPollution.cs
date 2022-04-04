using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPollution : MonoBehaviour
{
    private static VisualPollution _instance;
    public static VisualPollution Instance { get { return _instance; } }

    public GameObject DustParticlesPrefab;

    public Gradient pollutionGradient;

    private ParticleSystem particleSystem;
    private int startingMaxParticles;


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

    private void Start()
    {
        Instantiate(DustParticlesPrefab);
        StartingVisuals();
        SetDustParticles();
    }

    private void StartingVisuals()
    {
        float slideValue = ProgressBar.Instance.GetSlideValue();
        RenderSettings.fogColor = pollutionGradient.Evaluate(slideValue / 100);
        RenderSettings.fogDensity = ((100f - slideValue) / (slideValue * 100));

    }

    public void UpdateVisualPollution(float sustainabilityPercentage)
    {
        if (sustainabilityPercentage < 1) sustainabilityPercentage = 1;
        if (sustainabilityPercentage > 99) sustainabilityPercentage = 99;

        UpdateFog(sustainabilityPercentage);
        UpdateDustParticles(sustainabilityPercentage);
    }

    private void UpdateFog(float sustainabilityPercentage)
    {
        Color color = pollutionGradient.Evaluate(sustainabilityPercentage / 100);
        RenderSettings.fogColor = color;
        RenderSettings.fogDensity = 0.02f * ((100f - sustainabilityPercentage) / 100); 
    }

    private void SetDustParticles()
    {
        particleSystem = DustParticlesPrefab.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule settings = particleSystem.main;
        startingMaxParticles = settings.maxParticles;
    }

    private void UpdateDustParticles(float sustainabilityPercentage)
    {
        //eg. sustainabilityPercentage == 100

        ParticleSystem.MainModule settings = particleSystem.main;
        var emission = particleSystem.emission;

        //Changing fequencu
        settings.maxParticles = (int) (startingMaxParticles / (sustainabilityPercentage/2));
        emission.rateOverTime = (startingMaxParticles / 5) / (sustainabilityPercentage/2);

        //Changing color
        Color color = pollutionGradient.Evaluate(sustainabilityPercentage / 100);
        color.a = (100f - sustainabilityPercentage) / (100 + 50);
        settings.startColor = color;
    }
}
