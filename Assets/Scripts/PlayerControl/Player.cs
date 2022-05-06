using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Player : MonoBehaviourPun
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

    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //miniGameBase = GetComponent<MiniGameBase>();
        oneOffUpgradeList = SetUpList();
        Hashtable hashtable = new Hashtable();
        hashtable.Add("ready", true);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
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


    // Update is called once per frame
    void Update()
    {
        
    }

    public OneOffUpgrade GetUpgrade(OneOffUpgradesEnum upgrade)
    {
        return oneOffUpgradeList.Where(x => x.Upgrade == upgrade).FirstOrDefault();
    }
}
