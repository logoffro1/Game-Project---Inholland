using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningShoes : Equipment
{
    [SerializeField] private PlayerMovement playerMovement;
    public override void DoAction()
    {
        if (isActive) cooldown = maxCooldown - activeTime;
        activeTime = 15f;

        isActive = !isActive;

        drainOverTime = isActive;
        playerMovement.SpeedBoost(isActive);

/*        if (isActive)
            audioSource.PlayOneShot(xrayOn);
        else
            audioSource.PlayOneShot(xrayOff);*/
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (cooldown <= 0)
                DoAction();
        }
        if (!isActive)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                    cooldown = 0;
            }
        }

        DrainTime();
    }
}
