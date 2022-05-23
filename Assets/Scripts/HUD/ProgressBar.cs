using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon;

//This class is used for the proggress bar slider to determine the progression of the game. It is used in many classes for tracking the state of the game.
public class ProgressBar : MonoBehaviourPun, IPunObservable, IPunOwnershipCallbacks
{

    
    [SerializeField] private Text _SliderText;

    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private float sliderThreshhold;

    public bool isGameOngoing;

    public float AmountDecreasingPerSecond = -0.0005f;

    private static ProgressBar _instance;
    public static ProgressBar Instance { get { return _instance; } }

    private bool inCoroutine = false;

    //Initialize the fields
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
        PhotonNetwork.AddCallbackTarget(_instance);
    }
    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(_instance);
    }
    public float GetSliderMaxValue()
    {
        return slider.maxValue;
    }
    //These values are optimal for a good gaming experience. However, it can be changed for different purposes.
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
        if (photonView == null || !photonView.IsMine) return;
        if (isGameOngoing)
        {
            DecreaseSustainibilityPerSecond(AmountDecreasingPerSecond);
        }
    }

    private void UpdateProgressPercent()
    {
        _SliderText.text = slider.value.ToString("0.00") + "%";
    }

    //This method allows for sustainibility bar to  decrease a small amount. 
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
    //Animation for slider. It only waits for mini games because of the success fail animation they would miss the progress bar animation. isMiniGame check is done for better user experience.
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
    //Main method for changing the sustainibility. Often used upon win or lose conditions, soft tasks.
    public void ChangeSustainibility(float sustainabilityChange, bool isMiniGame)
    {
        base.photonView.RequestOwnership();

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
        }
        else if(stream.IsReading)
        {
            slider.value = (float)stream.ReceiveNext();
            UpdateProgressPercent();
        }
    }

    public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
    {
        if (targetView != base.photonView)
            return;

        base.photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
    {
        if (targetView != base.photonView)
            return;
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Photon.Realtime.Player senderOfFailedRequest)
    {
        //throw new System.NotImplementedException();
        Debug.Log("FAILED OWNERSHIP TRANSFER - PROGRESS BAR");
    }
}
