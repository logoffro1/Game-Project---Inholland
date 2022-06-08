using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

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
            FindObjectOfType<GlobalAchievements>().GetAchievement("forest").CurrentCount++;
        }
        else
            audioSource.PlayOneShot(shoesOff);
    }

    public override void SetPlayerRep()
    {
        foreach (PlayerReputation pr in FindObjectsOfType<PlayerReputation>())
        {
            if (pr.photonView.IsMine)
                playerRep = pr;
        }
        SetLocked(playerRep.IsShoeLocked);
    }
    void Start()
    {
        InitInfo();
    }
    private void InitInfo()
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().photonView.IsMine)
            {
                playerMovement = p.GetComponent<PlayerMovement>();
                break;
            }
        }
    }
    void Update()
    {
        if (IsLocked) return;
        if (SceneManager.GetActiveScene().name == "NewOffice") return;
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!isActive && cooldown <= 0)
                DoAction();
            else if (isActive)
                DoAction();
        }

        // control cooldown
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
