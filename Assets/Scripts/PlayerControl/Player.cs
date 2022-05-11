using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    private bool host = true;
    public bool Host { get { return host;  } }
    private string name;
    private List<OneOffUpgrade> oneOffUpgradeList;
    public List<OneOffUpgrade> OneOffUpgradeList { get { return oneOffUpgradeList; } }
    private float minigameLevel;

    //All necessary components
    private PlayerMovement playerMovement;
    //private MiniGameBase miniGameBase;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //miniGameBase = GetComponent<MiniGameBase>();
        oneOffUpgradeList = SetUpList(); 
    }

    public List<OneOffUpgrade> SetUpList()
    {
        //TODO: Change how this is gotten
        MiniGameBase miniGameBase = FindObjectOfType<MiniGameBase>();

        List<OneOffUpgrade> list = new List<OneOffUpgrade>();
        foreach (OneOffUpgradesEnum upgrade in (OneOffUpgradesEnum[])Enum.GetValues(typeof(OneOffUpgradesEnum)))
        {
            list.Add(new OneOffUpgrade(upgrade, this, GetComponent<PlayerMovement>(), miniGameBase));
        }

        return list;
    }


    public OneOffUpgrade GetUpgrade(OneOffUpgradesEnum upgrade)
    {
        return oneOffUpgradeList.Where(x => x.Upgrade == upgrade).FirstOrDefault();
    }
}
