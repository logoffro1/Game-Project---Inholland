using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualPollution : MonoBehaviour
{
    private static VisualPollution _instance;
    public static VisualPollution Instance { get { return _instance; } }

    public GameObject DustParticlesPrefab;

    public Gradient pollutionGradient;
    public Color staticColor;

    private ParticleSystem particleSystem;
    private int startingMaxParticles = 50000;

    private List<MeshRenderer> waters;
    public Gradient waterGradient;


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
        SetCanal();
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
        UpdateCanalColor(sustainabilityPercentage);
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
        UpdateDustParticles(20);
    }

    private void UpdateDustParticles(float sustainabilityPercentage)
    {
        //eg. sustainabilityPercentage == 100

        ParticleSystem.MainModule settings = particleSystem.main;
        var emission = particleSystem.emission;
        var colorOverLifetime = particleSystem.colorOverLifetime;

        //Changing fequencu
        //settings.maxParticles = (int) (startingMaxParticles / (sustainabilityPercentage/2));
        //emission.rateOverTime = (startingMaxParticles / 5) / (sustainabilityPercentage/2);

        //particleSystem.Emit((int)(100f - sustainabilityPercentage) * 100); 

        //Changing color
        //Color color = pollutionGradient.Evaluate(sustainabilityPercentage / 100);
        //color.a = (100f - sustainabilityPercentage) / (100 + 150);

        //settings.startColor = staticColor;
        //colorOverLifetime.color = staticColor;
    }

    private void SetCanal()
    {
        waters = GameObject.FindGameObjectWithTag("Water").GetComponentsInChildren<MeshRenderer>().ToList();
    }

    private void UpdateCanalColor(float sustainabilityPercentage)
    {
        Color color = waterGradient.Evaluate(sustainabilityPercentage / 100);
        color.a = 1;
        foreach(MeshRenderer water in waters)
        {
            water.material.color = color;
        }
    }
}
