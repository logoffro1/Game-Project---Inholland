using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        oneOffUpgradeList = OneOffUpgrade.SetUpList(); 
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
