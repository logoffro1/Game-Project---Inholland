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
    private Volume xrayVolume;

    // Start is called before the first frame update
    void Start()
    {
        xrayVolume = GetComponent<Volume>();
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
        xrayVolume.enabled = xrayRenderer.isActive;
        if (xrayRenderer.isActive)
            StartCoroutine(AnimateVignette());
        foreach (InteractableTaskObject obj in GameObject.FindObjectsOfType<InteractableTaskObject>())
        {
            if (xrayRenderer.isActive)
                obj.gameObject.layer = 8;
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
