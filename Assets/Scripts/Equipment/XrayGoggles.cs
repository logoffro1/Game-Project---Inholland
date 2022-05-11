using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class XrayGoggles : Equipment
{
    [SerializeField]
    private XRayVision xrayVision;

    [SerializeField] private AudioClip xrayOn;
    [SerializeField] private AudioClip xrayOff;

    private AudioSource audioSource;

    public override void DoAction()
    {
        activeTime = 15f;

        if (xrayVision == null) return;
        CastRay.Instance.CanInteract = isActive;
        isActive = !isActive;

        drainOverTime = isActive;
        xrayVision.ActivateVision();

        if (isActive)
        {
            audioSource.PlayOneShot(xrayOn);
            FindObjectOfType<GlobalAchievements>().GetAchievement("Wallhacks").CurrentCount++;
        }
           
        else
            audioSource.PlayOneShot(xrayOff);
    }

    void Start()
    {      
        drainOverTime = false;
        isActive = false;
        equipmentName = "XRAY Goggles";
        activeTime = 15f;
        maxCooldown = 15f;
        cooldown = maxCooldown;

        audioSource = GetComponent<AudioSource>();
    }

    public override void SetPlayerRep()
    {
        playerRep = FindObjectOfType<PlayerReputation>();
        SetLocked(playerRep.IsXrayLocked);
    }
  

    void Update()
    {


        if (isLocked) return;
        if (SceneManager.GetActiveScene().name == "NewOffice") return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!isActive && cooldown <= 0)
                DoAction();
            else if (isActive)
                DoAction();
        }
        if(!isActive)
        {
            if(cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                    cooldown = 0;

                onCooldownChange(this);
            }
        }
        else
        {
            cooldown += (Time.deltaTime);
            if (cooldown >= maxCooldown)
                cooldown = maxCooldown;
            onCooldownChange(this);
        }
        DrainTime();
    }

}
