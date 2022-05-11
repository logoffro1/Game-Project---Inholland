using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningShoes : Equipment
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip shoesOn;
    [SerializeField] private AudioClip shoesOff;
    [SerializeField] private PlayerMovement playerMovement;
    public override void DoAction()
    {
        activeTime = 7f;

        isActive = !isActive;

        drainOverTime = isActive;
        playerMovement.SpeedBoost(isActive);

        if (isActive)
        {
            audioSource.PlayOneShot(shoesOn);
            FindObjectOfType<GlobalAchievements>().GetAchievement("Run Forest, Run!").CurrentCount++;
        }
        else
            audioSource.PlayOneShot(shoesOff);
    }

    public override void SetPlayerRep()
    {
        playerRep = FindObjectOfType<PlayerReputation>();
        SetLocked(playerRep.IsShoeLocked);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRep = FindObjectOfType<PlayerReputation>();
        this.SetLocked(playerRep.IsShoeLocked);
        audioSource = GetComponent<AudioSource>();
        drainOverTime = false;
        isActive = false;
        equipmentName = "Running Shoes";
        activeTime = 7;
        maxCooldown = 30f;
        cooldown = maxCooldown;
    }

    // Update is called once per frame
    void Update()
    {

        if (isLocked) return;
        if (SceneManager.GetActiveScene().name == "NewOffice") return;
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!isActive && cooldown <= 0)
                DoAction();
            else if (isActive)
                DoAction();
        }
        if (!isActive)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                    cooldown = 0;

                onCooldownChange(this);
            }
        }
        else
        {
            cooldown += (4f * Time.deltaTime);
            if (cooldown >= maxCooldown)
                cooldown = maxCooldown;
            onCooldownChange(this);
        }

        DrainTime();
    }
}
