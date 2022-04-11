using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class XRayVision : MonoBehaviour
{
    [SerializeField] private RenderObjects normalRenderer;
    [SerializeField] private RenderObjects xrayRenderer;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image batteryFullImage;
    private Volume xrayVolume;

    private bool drainOverTime = true;
    [SerializeField] private float drainRate = 1f;
    private float batteryLevel = 100f;

    private bool flashing = false;
    // Start is called before the first frame update
    void Start()
    {
        xrayVolume = GetComponent<Volume>();

        normalRenderer.SetActive(true);
        xrayRenderer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateVision();
        }
        if (xrayVolume.enabled)
        {
            DrainBattery();
        }

    }
    private void DrainBattery()
    {
        if (drainOverTime)
        {
            batteryLevel -= Time.deltaTime * drainRate;
            batteryFullImage.fillAmount = (batteryLevel / 100) - 0.1f;
            Debug.Log(batteryLevel / 100);
            if(batteryLevel <= 45 && !flashing)
            {
                if(batteryLevel > 0)
                  StartCoroutine(FlashBattery());
            }
            if (batteryLevel <= 0)
            {
                batteryLevel = 0;
                drainOverTime = false;
                ActivateVision();
            }
        }
    }
  private IEnumerator FlashBattery()
    {
        
        flashing = true;
        float alpha = 255;
        while (alpha>=100)
        {
            batteryFullImage.color = new Color32(255, 255, 255, (byte)alpha--);
            
            Debug.Log(batteryFullImage.color.a);
            yield return null;
        }
        while(alpha < 255)
        {
            batteryFullImage.color = new Color32(255, 255, 255, (byte)alpha++);

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        flashing = false;
    }
    private void ActivateVision()
    {
        if (batteryLevel <= 0 && !xrayVolume.enabled) return;
        normalRenderer.SetActive(!normalRenderer.isActive);
        xrayRenderer.SetActive(!xrayRenderer.isActive);
        xrayVolume.enabled = xrayRenderer.isActive;
        canvas.enabled = xrayVolume.enabled;
        UIManager.Instance.ChangeCanvasShown();

        if (xrayRenderer.isActive)
            StartCoroutine(AnimateVignette());
        foreach (InteractableTaskObject obj in GameObject.FindObjectsOfType<InteractableTaskObject>())
        {
            if (xrayRenderer.isActive)
                obj.gameObject.layer = 11;
            else
                obj.gameObject.layer = 0;
        }

    }

    private IEnumerator AnimateVignette()
    {
        Vignette vg;
        DepthOfField dof;
        xrayVolume.profile.TryGet(out vg);
        xrayVolume.profile.TryGet(out dof);
        vg.intensity.value = 1f;
        dof.active = true;
        dof.focusDistance.value = 1;
        //  vg.center.value = new Vector2(0.5f, -0.1f);

        while (true)
        {
            vg.intensity.value -= 0.01f;
            // vg.center.value = new Vector2(0.5f, vg.center.value.y + 0.01f);
            dof.focusDistance.value += 0.1f;
            if (vg.intensity.value <= 0.3f) break;
            yield return new WaitForSeconds(0.01f);
        }
        dof.active = false;
    }
}
