using UnityEngine;
using UnityEngine.SceneManagement;
public class XrayGoggles : Equipment
{
    [SerializeField]
    private XRayVision xrayVision; // xray post processing effects

    [SerializeField] private AudioClip xrayOn;
    [SerializeField] private AudioClip xrayOff;

    private AudioSource audioSource;

    public override void DoAction() // turn xray ON / OFF
    {
        activeTime = 15f;

        if (xrayVision == null) return;
        CastRay.Instance.CanInteract = isActive; // set interacting off while in xray mode
        isActive = !isActive;

        drainOverTime = isActive;
        xrayVision.ActivateVision();

        if (isActive) // play on / off sounds
        {
            audioSource.PlayOneShot(xrayOn);
            FindObjectOfType<GlobalAchievements>().GetAchievement("wallhacks").CurrentCount++; // increase achievement
        }
           
        else
            audioSource.PlayOneShot(xrayOff);
    }
    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
        SetPlayerRep();
        if(SceneManager.GetActiveScene().name != "NewOffice") // check scene before getting the post processing
            xrayVision = GameObject.FindGameObjectWithTag("xray").GetComponent<XRayVision>();
    }
    void Start()
    {
        InitInfo();

        audioSource = GetComponent<AudioSource>();
    }
   private void InitInfo()
    {
        drainOverTime = false;
        isActive = false;
        equipmentName = "XRAY Goggles";
        activeTime = 15f;
        maxCooldown = 15f;
        cooldown = maxCooldown;
    }
    public override void SetPlayerRep()
    {
        foreach(PlayerReputation pr in FindObjectsOfType<PlayerReputation>())
        {
            if (pr.photonView.IsMine)
                playerRep = pr;
        }
        SetLocked(playerRep.IsXrayLocked);
    }
    void Update()
    {
        if (IsLocked) return;
        if (SceneManager.GetActiveScene().name == "NewOffice") return; // dont allow use in the office

        if (Input.GetKeyDown(KeyCode.Alpha1)) // if key '1' is pressed
        {
            if (!isActive && cooldown <= 0)
                DoAction();
            else if (isActive)
                DoAction();
        }
        if(!isActive) // if its inactive
        {
            if(cooldown > 0)
            { // decrease the cooldown until its's 0 and ready to use again
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                    cooldown = 0;

                onCooldownChange(this);
            }
        }
        else // if its active
        {
            // increase the cooldown over time
            // the cooldown is based on how much the player uses the goggles
            cooldown += (Time.deltaTime);
            if (cooldown >= maxCooldown)
                cooldown = maxCooldown;
            onCooldownChange(this);
        }
        DrainTime();
    }

}
