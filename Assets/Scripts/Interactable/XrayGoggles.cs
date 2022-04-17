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
    [SerializeField] private float chargeRate = 2f;
    public float BatteryLevel { get; private set; } = 100f;

    public bool IsEquipped { get; set; } = false;
    private bool isActive = false;
    public bool IsCharging { get; set; } = false;

    public Action<float> OnBatteryLevelChange;
    public override void DoAction(GameObject player)
    {
        IsEquipped = true;
        transform.position = player.transform.position;
        transform.parent = player.transform.parent;
        GetComponent<MeshRenderer>().enabled = false;
        foreach (MeshRenderer m in gameObject.GetComponentsInChildren<MeshRenderer>())
            m.enabled = false;
        foreach (BoxCollider b in GetComponents<BoxCollider>())
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
        ChargeBattery();
    }
    private void Activate()
    {

        if (xrayVision == null || !IsEquipped || BatteryLevel <= 0f) return;
        isActive = !isActive;
        drainOverTime = isActive;

        xrayVision.ActivateVision();
    }
    private void DrainBattery()
    {
        if (BatteryLevel > 0)
            drainOverTime = true;
        if (drainOverTime && isActive)
        {
            BatteryLevel -= Time.deltaTime * drainRate;

            if (BatteryLevel <= 0)
            {
                BatteryLevel = 0;
                isActive = false;
                drainOverTime = false;
            }
            OnBatteryLevelChange(BatteryLevel);
        }
    }
    private void ChargeBattery()
    {
        if (IsCharging)
        {
            BatteryLevel += Time.deltaTime * drainRate;
            if (BatteryLevel >= 100)
            {
                BatteryLevel = 100;
                IsCharging = false;
            }
        }
    }
}
