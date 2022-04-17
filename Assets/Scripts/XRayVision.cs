using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class XRayVision : MonoBehaviour
{
    [SerializeField] private RenderObjects normalRenderer;
    [SerializeField] private RenderObjects xrayRenderer;
    [SerializeField] private RenderObjects trashRenderer;
    private Volume xrayVolume;

    // Start is called before the first frame update
    void Start()
    {
        xrayVolume = GetComponent<Volume>();

        normalRenderer.SetActive(true);
        xrayRenderer.SetActive(false);
        trashRenderer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateVision();
        }
    }
    private void ActivateVision()
    {
        normalRenderer.SetActive(!normalRenderer.isActive);
        xrayRenderer.SetActive(!xrayRenderer.isActive);
        trashRenderer.SetActive(!trashRenderer.isActive);
        xrayVolume.enabled = xrayRenderer.isActive;
        if (xrayRenderer.isActive)
            StartCoroutine(AnimateVignette());
        foreach (InteractableTaskObject obj in GameObject.FindObjectsOfType<InteractableTaskObject>())
        {
            if (xrayRenderer.isActive)
                obj.gameObject.layer = 11;
            else
                obj.gameObject.layer = 0;
        }
        foreach(Trash trash in GameObject.FindObjectsOfType<Trash>())
        {
            if (xrayRenderer.isActive)
                trash.gameObject.layer = 12;
            else
                trash.gameObject.layer = 0;
        }
        foreach(Dumpster dumpster in GameObject.FindObjectsOfType<Dumpster>())
        {
            if (xrayRenderer.isActive)
                dumpster.gameObject.layer = 12;
            else
                dumpster.gameObject.layer = 0;
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
