using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    protected float activeTime;
    protected float cooldown;
    protected float maxCooldown;
    protected bool isActive;
    protected bool drainOverTime;

    protected string equipmentName;

    public bool isLocked { get; set; }

    public abstract void DoAction();

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
