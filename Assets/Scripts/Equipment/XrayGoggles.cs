using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class XrayGoggles : Equipment
{
    [SerializeField]
    private XRayVision xrayVision;

    [SerializeField] private AudioClip xrayOn;
    [SerializeField] private AudioClip xrayOff;
    private AudioSource audioSource;

    public override void DoAction()
    {
        if (isActive) cooldown = maxCooldown - activeTime;
        activeTime = 15f;

        if (xrayVision == null) return;
        CastRay.Instance.CanInteract = isActive;
        isActive = !isActive;

        drainOverTime = isActive;
        xrayVision.ActivateVision();

        if (isActive)
            audioSource.PlayOneShot(xrayOn);
        else
            audioSource.PlayOneShot(xrayOff);
    }

    // Start is called before the first frame update
    void Start()
    {
        drainOverTime = false;
        isActive = false;
        equipmentName = "XRAY Goggles";
        activeTime = 15f;
        maxCooldown = 15f;
        cooldown = maxCooldown;
        //SetLocalizedString(this.localizedStringEvent);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (isLocked) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(cooldown <= 0)
             DoAction();
        }
        if(!isActive)
        {
            if(cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                    cooldown = 0;
            }
        }
        DrainTime();
    }

}
