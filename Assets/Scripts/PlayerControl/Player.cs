using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Player : MonoBehaviourPun
{
    private bool host = true;
    public bool Host { get { return host; } }
    private string name;
    private List<OneOffUpgrade> oneOffUpgradeList;
    public List<OneOffUpgrade> OneOffUpgradeList { get { return oneOffUpgradeList; } }
    private float minigameLevel;
    [SerializeField] MouseLook mouseLook;

    //All necessary components
    private PlayerMovement playerMovement;
    //private MiniGameBase miniGameBase;

    public static GameObject LocalPlayerInstance;

    [SerializeField] private TextMeshProUGUI trashText;
    [SerializeField] private TextMeshProUGUI flyersText;
    [SerializeField] private Image trashFill;
    [SerializeField] private Canvas playerCanvas;

    private void Awake()
    {
        
        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
            DontDestroyOnLoad(this.gameObject);
    }
    private void OnLevelWasLoaded()
    {

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if(PhotonNetwork.NetworkClientState != Photon.Realtime.ClientState.Leaving)
                PhotonNetwork.LeaveRoom();
            DestroyImmediate(gameObject);
            return;
        }
        CastRay.Instance.CanInteract = true;
        //Discord status change happens on every scene change before LoadSceneAsync();
        if (DiscordController.Instance.IsDiscordRunning())
        {
            StatusType type = (StatusType)Enum.Parse(typeof(StatusType), SceneManager.GetActiveScene().name);
            DiscordController.Instance.UpdateDiscordStatus(type);
            Debug.Log(type);
        }

        SpawnPlayer spawnPlayer = FindObjectOfType<SpawnPlayer>();
        transform.position = spawnPlayer.transform.position;

        if (SceneManager.GetActiveScene().name == "NewOffice")
        {
            GetComponent<PlayerMovement>().canMove = true;
        }
        else {
            StartCoroutine(StartListDelay());
        }
        UIManager.Instance.SetPlayerInfo(trashText,flyersText,playerCanvas,trashFill);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseLook.canR = true;
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //miniGameBase = GetComponent<MiniGameBase>();
        Hashtable hashtable = new Hashtable();
        hashtable.Add("ready", true);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
    }

    private IEnumerator StartListDelay()
    {
        yield return new WaitForSeconds(1);
        oneOffUpgradeList = SetUpList();
    }
    public List<OneOffUpgrade> SetUpList()
    {
        //TODO: Change how this is gotten
        MiniGameBase miniGameBase = FindObjectOfType<MiniGameBase>();

        List<OneOffUpgrade> list = new List<OneOffUpgrade>();
        OneOffUpgradeContent[] contentList = FindObjectsOfType<OneOffUpgradeContent>();
        foreach (OneOffUpgradesEnum upgrade in (OneOffUpgradesEnum[])Enum.GetValues(typeof(OneOffUpgradesEnum)))
        {
            list.Add(new OneOffUpgrade(upgrade, contentList, this, GetComponent<PlayerMovement>(), miniGameBase));
        }

        return list;
    }

    public OneOffUpgrade GetUpgrade(OneOffUpgradesEnum upgrade)
    {
        return oneOffUpgradeList.Where(x => x.Upgrade == upgrade).FirstOrDefault();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Church"))
        {
            FindObjectOfType<GlobalAchievements>().GetAchievement("church").CurrentCount++;
        }
    }
}
