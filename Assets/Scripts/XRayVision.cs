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
    [SerializeField] private RenderObjects trashRenderer;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image batteryFullImage;
    private Volume xrayVolume;

    private bool flashing = false;
    // Start is called before the first frame update
    void Start()
    {
        xrayVolume = GetComponent<Volume>();

        normalRenderer.SetActive(true);
        xrayRenderer.SetActive(false);
        trashRenderer.SetActive(false);
    }

    public void BatteryChanged(float batteryLevel)
    {
        batteryFullImage.fillAmount = (batteryLevel / 100);
        if (batteryLevel <= 35 && !flashing)
        {
            if (batteryLevel > 0 && xrayVolume.enabled)
                StartCoroutine(FlashBattery());
        }
    }

    private IEnumerator FlashBattery()
    {

        flashing = true;
        float alpha = 255;
        float GB = 255;
        while (alpha >= 100)
        {
            batteryFullImage.color = new Color32(255, (byte)GB, (byte)GB, (byte)alpha--);
            GB--;
            yield return null;
        }
        while (alpha < 255)
        {
            batteryFullImage.color = new Color32(255, (byte)GB, (byte)GB, (byte)alpha++);
            GB++;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        flashing = false;
    }
    public void ActivateVision()
    {

        normalRenderer.SetActive(!normalRenderer.isActive);
        xrayRenderer.SetActive(!xrayRenderer.isActive);
        trashRenderer.SetActive(!trashRenderer.isActive);
        canvas.enabled = xrayRenderer.isActive;

        UIManager.Instance.ChangeCanvasShown();

        if (xrayRenderer.isActive)
            StartCoroutine(AnimateVignette(1f, 0.4f));
        else
            StartCoroutine(AnimateVignette(1f, 1f));

        foreach (InteractableTaskObject obj in GameObject.FindObjectsOfType<InteractableTaskObject>())
        {
            if (xrayRenderer.isActive)
                obj.gameObject.layer = 11;
            else
                obj.gameObject.layer = 0;
        }
        foreach (Trash trash in GameObject.FindObjectsOfType<Trash>())
        {
            if (xrayRenderer.isActive)
                trash.gameObject.layer = 12;
            else
                trash.gameObject.layer = 0;
        }
        foreach (Dumpster dumpster in GameObject.FindObjectsOfType<Dumpster>())
        {
            if (xrayRenderer.isActive)
                dumpster.gameObject.layer = 12;
            else
                dumpster.gameObject.layer = 0;
        }

    }

    private IEnumerator AnimateVignette(float start, float target)
    {
        xrayVolume.enabled = true;

        Vignette vg;
        DepthOfField dof;
        xrayVolume.profile.TryGet(out vg);
        xrayVolume.profile.TryGet(out dof);
        vg.intensity.value = start;
        dof.active = true;
        dof.focusDistance.value = start;
        //  vg.center.value = new Vector2(0.5f, -0.1f);

        while (true)
        {
            if (target < start)
            {
                vg.intensity.value -= 0.01f;
                dof.focusDistance.value += 0.1f;
                if (vg.intensity.value <= target) break;
            }
            else
            {
                vg.intensity.value += 0.022f;
                if (vg.intensity.value >= target) break;
            }

            yield return new WaitForSeconds(0.01f);
        }
        dof.active = false;
        if (!xrayRenderer.isActive)
            xrayVolume.enabled = false;
    }
}
