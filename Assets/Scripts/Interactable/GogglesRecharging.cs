using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GogglesRecharging : InteractableObject
{

    [SerializeField]
    private Light statusLight;
    [SerializeField]
    private GameObject glasses;
    private XrayGoggles goggles = null;

    private RechargingStates currentStatus = RechargingStates.Full;
    // Start is called before the first frame update
    void Start()
    {
        hoverName = "Recharge Goggles";
        ChangeStatus(RechargingStates.Empty);
    }

    // Update is called once per frame
    void Update()
    {
       if(goggles != null)
        {
            hoverName = $"Recharging ({goggles.BatteryLevel.ToString("0.0")}%)";
        }
        else
        {
            hoverName = "Recharge Goggles";
        }
        ChangeStatus(GetStatus());
    }
    public override void DoAction(GameObject player)
    {
        if (goggles == null)
        {
            goggles = player.transform.parent.GetComponentInChildren<XrayGoggles>();
            if (goggles != null)
            {
                goggles.IsEquipped = false;
            }
        }

        else
        {
            if (goggles != null)
            {
                if (!goggles.IsEquipped)
                {
                    goggles.IsCharging = false;
                    goggles.IsEquipped = true;
                }
            }
            goggles = null;

        }

        ChangeStatus(GetStatus());
    }
    private RechargingStates GetStatus()
    {
        if (goggles == null) return RechargingStates.Empty;

        if (goggles.BatteryLevel < 100f)
            return RechargingStates.Charging;
        else
            return RechargingStates.Full;

    }
    private void ChangeStatus(RechargingStates status)
    {
        Debug.Log(status.ToString());
        if (status == currentStatus) return;
        currentStatus = status;
        switch (status)
        {
            case RechargingStates.Empty:
                statusLight.enabled = false;
                break;
            case RechargingStates.Charging:
                statusLight.enabled = true;
                statusLight.color = Color.red;
                goggles.IsCharging = true;
                break;
            case RechargingStates.Full:
                statusLight.enabled = true;
                statusLight.color = Color.green;
                break;
        }
        glasses.SetActive(statusLight.enabled);
    }
}
