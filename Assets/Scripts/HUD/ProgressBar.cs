using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ProgressBar : MonoBehaviourPun, IPunObservable
{

    [SerializeField] private Text _SliderText;

    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private float sliderThreshhold;

    public bool isGameOngoing;



    private static ProgressBar _instance;
    public static ProgressBar Instance { get { return _instance; } }

    private bool inCoroutine = false;
    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        isGameOngoing = true;
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
    public float GetSliderMaxValue()
    {
        return slider.maxValue;
    }

    private void SliderInit()
    {
        slider.maxValue = 100f;
        slider.minValue = 0f;
        sliderThreshhold = 20f;
        slider.value = 40f;
        fill.color = gradient.Evaluate(0.1f);
        _SliderText.text = slider.value.ToString("0.00") + "%";
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (isGameOngoing)
        {
            DecreaseSustainibilityPerSecond(-0.0005f);
        }
    }

    private void UpdateProgressPercent()
    {
        _SliderText.text = slider.value.ToString("0.00") + "%";
    }

    private void DecreaseSustainibilityPerSecond(float sustainibilityValue)
    {
        if (inCoroutine) return;
        if (slider.value > sliderThreshhold)
        {
            slider.value += sustainibilityValue;
            fill.color = gradient.Evaluate(slider.normalizedValue);

        }
        UpdateProgressPercent();
    }

    private IEnumerator ApplySliderAnimation(float target, bool isMiniGame)
    {
        inCoroutine = true;
        if (isMiniGame)
        {
            yield return new WaitForSeconds(2.5f);
        }
        float t = 0.0f;
        float elapsedTime = 0.0f;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(slider.value, target, elapsedTime / waitTime);
            t += 0.5f * elapsedTime;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            UpdateProgressPercent();
            yield return null;

        }
        inCoroutine = false;
    }

    public void ChangeSustainibility(float sustainabilityChange, bool isMiniGame)
    {
        /*        slider.value += sustainabilityChange;
                fill.color = gradient.Evaluate(slider.normalizedValue);*/
        StartCoroutine(ApplySliderAnimation(slider.value + sustainabilityChange, isMiniGame));
        UpdateProgressPercent();
    }

    public float GetSlideValue()
    {
        if (slider == null) return 0;
        return slider.value;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(slider.value);
            UpdateProgressPercent();
        }
        else if(stream.IsReading)
        {
            slider.value = (float)stream.ReceiveNext();
            UpdateProgressPercent();
        }
    }
}
