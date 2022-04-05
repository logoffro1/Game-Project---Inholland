using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


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

    private List<GameObject> allAnimals;

    private ColorAdjustments colorAdjustments;

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
        SetAnimals();
        SetPostProcessing();
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
        //UpdateDustParticles(sustainabilityPercentage);
        UpdateCanalColor(sustainabilityPercentage);
        UpdateAnimals(sustainabilityPercentage);
        UpdatePostProcessing(sustainabilityPercentage);
    }

    private void UpdateFog(float sustainabilityPercentage)
    {
        Color color = pollutionGradient.Evaluate(sustainabilityPercentage / 100);
        RenderSettings.fogColor = color;
        RenderSettings.fogDensity = 0.012f * ((100f - sustainabilityPercentage) / 100); 
    }

    private void SetDustParticles()
    {
        particleSystem = DustParticlesPrefab.GetComponent<ParticleSystem>();
        UpdateDustParticles(ProgressBar.Instance.GetSlideValue());
    }

    private void UpdateDustParticles(float sustainabilityPercentage)
    {
        //eg. sustainabilityPercentage == 100

        ParticleSystem.MainModule settings = particleSystem.main;
        var emission = particleSystem.emission;
        var colorOverLifetime = particleSystem.colorOverLifetime;

        //Changing fequencu
        settings.maxParticles = (int) (startingMaxParticles / (sustainabilityPercentage/4));
        emission.rateOverTime = (startingMaxParticles / 5) / (sustainabilityPercentage/4);

        //particleSystem.Emit((int)(100f - sustainabilityPercentage) * 100); 

        //Changing color
        Color color = pollutionGradient.Evaluate(sustainabilityPercentage / 100);
        color.a = (100f - sustainabilityPercentage) / (100 + 150);

        settings.startColor = color;
        colorOverLifetime.color = color;
    }

    private void SetCanal()
    {
        waters = GameObject.FindGameObjectWithTag("Water").GetComponentsInChildren<MeshRenderer>().ToList();
        UpdateCanalColor(ProgressBar.Instance.GetSlideValue());
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

    private void SetAnimals()
    {
        allAnimals = new List<GameObject>();

        foreach (Transform child in GameObject.FindGameObjectWithTag("Animal").transform)
        {
            allAnimals.Add(child.gameObject);
        }

        //Randomsize it
        int n = allAnimals.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject value = allAnimals[k];
            allAnimals[k] = allAnimals[n];
            allAnimals[n] = value;
        }

        UpdateAnimals(ProgressBar.Instance.GetSlideValue());
    }

    private void UpdateAnimals(float sustainabilityPercentage)
    {
        if (sustainabilityPercentage >= 95)
        {
            ActivateAnimal(100);
        }
        else if (sustainabilityPercentage >= 90)
        {
            ActivateAnimal(80);
        }
        else if (sustainabilityPercentage >= 80)
        {
            ActivateAnimal(60);
        }
        else if(sustainabilityPercentage >= 70)
        {
            ActivateAnimal(40);

        }
        else if (sustainabilityPercentage >= 60)
        {
            ActivateAnimal(20);
        }
        else
        {
            ActivateAnimal(0);
        }
    }

    private void ActivateAnimal(float percentage)
    {
        int limit = (int) (allAnimals.Count * (percentage / 100));

        //activate
        for (int i = 0; i < limit; i++)
        {
            allAnimals[i].gameObject.SetActive(true);
        }

        //deactivtae
        for (int i = limit; i < allAnimals.Count; i++)
        {
            allAnimals[i].gameObject.SetActive(false);
        }
     }

    private void SetPostProcessing()
    {
        if (FindObjectOfType<Volume>().profile.TryGet(out ColorAdjustments colorA))
        {
            colorAdjustments = colorA;
        }

        UpdatePostProcessing(ProgressBar.Instance.GetSlideValue());
    }

    public void UpdatePostProcessing(float sustainabilityPercentage)
    {
        float percentage = sustainabilityPercentage / 100;

        if (colorAdjustments != null)
        {
            if (percentage <= 0.5)
            {
                sustainabilityPercentage += -1;
            }

            //post exposure -1 1 -> dif == 2
            colorAdjustments.postExposure.SetValue(new VolumeParameter<float>() { value = GetPostProcessingValue(-1, 1, sustainabilityPercentage) });

            //contrast 5 15
            colorAdjustments.contrast.SetValue(new VolumeParameter<float>() { value = GetPostProcessingValue(5, 15, sustainabilityPercentage) });

            //saturation -12, 30
            colorAdjustments.saturation.SetValue(new VolumeParameter<float>() { value = GetPostProcessingValue(-12, 30, sustainabilityPercentage) });
        }
    }

    private float GetPostProcessingValue(float lowestPoint, float highestPoint, float sustainabilityPercentage)
    {
        float halfDifference = Mathf.Abs(highestPoint - lowestPoint)/2;
        float midPoint = lowestPoint + halfDifference;

        return midPoint + (halfDifference * (sustainabilityPercentage / 100));
    }
}
