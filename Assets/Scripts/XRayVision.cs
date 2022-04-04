using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
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
        foreach (InteractableTaskObject obj in GameObject.FindObjectsOfType<InteractableTaskObject>())
        {
            if (xrayRenderer.isActive)
                obj.gameObject.layer = 8;
            else
                obj.gameObject.layer = 0;
        }
    }
}
