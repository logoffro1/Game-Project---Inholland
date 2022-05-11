using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Equipment : MonoBehaviour
{
    protected float activeTime;
    public float cooldown { get; protected set; }
    public float maxCooldown { get; protected set; }
    protected bool isActive;
    protected bool drainOverTime;

    protected string equipmentName;

    public bool isLocked { get; set; }
    public Action<Equipment,bool> onLockedChange = delegate { };
    public Action<Equipment> onCooldownChange = delegate { };

    public abstract void DoAction();
    public virtual void SetLocked(bool locked)
    {
        isLocked = locked;
        onLockedChange(this,isLocked);
    }
    public virtual void DrainTime()
    {
        if (activeTime > 0)
            drainOverTime = true;
        if (drainOverTime && isActive)
        {
            activeTime -= Time.deltaTime;

            if (activeTime <= 0)
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
