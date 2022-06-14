using UnityEngine;
using System;
using Photon.Pun;


// All player equipment inherits from this class
public abstract class Equipment : MonoBehaviourPunCallbacks
{
    protected float activeTime;
    public float cooldown { get; protected set; }
    public float maxCooldown { get; protected set; }
    protected bool isActive;
    protected bool drainOverTime;
    protected PlayerReputation playerRep;


    protected string equipmentName;

    public bool IsLocked { get; set; }
    public Action<Equipment,bool> onLockedChange = delegate { };
    public Action<Equipment> onCooldownChange = delegate { };

    public abstract void DoAction();
    public abstract void SetPlayerRep();
    public virtual void SetLocked(bool locked) // set locked state
    {
        IsLocked = locked;
        onLockedChange(this,IsLocked);
    }
    public virtual void DrainTime() // drain the active time
    {

        if (activeTime > 0)
            drainOverTime = true;
        if (drainOverTime && isActive)
        {
            activeTime -= Time.deltaTime;

            if (activeTime <= 0) // if the active time is 0, turn off device
            {
                activeTime = 0;
                drainOverTime = false;
                if (isActive)
                {
                    DoAction();
                }
            }
        }
    }

}
