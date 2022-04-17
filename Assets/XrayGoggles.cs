using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class XrayGoggles : InteractableObject
{
    [SerializeField]
    private XRayVision xrayVision;

    private bool drainOverTime = false;
    [SerializeField] private float drainRate = 1f;
    private float batteryLevel = 100f;

    public bool IsEquipped { get; private set; } = false;
    private bool isActive = false;

    public Action<float> OnBatteryLevelChange = delegate { };
    public override void DoAction(GameObject player)
    {
        IsEquipped = true;
        transform.position = player.transform.position;
        transform.parent = player.transform.parent;
        GetComponent<MeshRenderer>().enabled = false;
        foreach (MeshRenderer m in gameObject.GetComponentsInChildren<MeshRenderer>())
            m.enabled = false;
        foreach(BoxCollider b in GetComponents<BoxCollider>())
        {
            b.enabled = false;
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        hoverName = "XRay Goggles";
        OnBatteryLevelChange += xrayVision.BatteryChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            Activate();

        DrainBattery();
    }
    private void Activate()
    {

        if (xrayVision == null || !IsEquipped) return;
        isActive = !isActive;
        drainOverTime = isActive;

        xrayVision.ActivateVision();
    }
    private void DrainBattery()
    {
        if (drainOverTime && isActive)
        {
            batteryLevel -= Time.deltaTime * drainRate;

            if (batteryLevel <= 0)
            {
                batteryLevel = 0;
                drainOverTime = false;
            }
            OnBatteryLevelChange(batteryLevel);
        }
    }
}
