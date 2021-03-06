using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Vacuum : Equipment
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip vacuumON;
    [SerializeField] private AudioClip vacuumOFF;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        drainOverTime = false;
        isActive = false;
        equipmentName = "Vacuum";
        activeTime = 10;
        maxCooldown = 15;
        cooldown = maxCooldown;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().photonView.IsMine)
            {
                player = p;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsLocked) return;
        if (SceneManager.GetActiveScene().name == "NewOffice") return;
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!isActive && cooldown <= 0)
                DoAction();
            else if (isActive)
                DoAction();
        }
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
            cooldown += (Time.deltaTime);
            if (cooldown >= maxCooldown)
                cooldown = maxCooldown;
            onCooldownChange(this);
        }
        DrainTime();
    }
    public override void SetPlayerRep()
    {
        foreach (PlayerReputation pr in FindObjectsOfType<PlayerReputation>())
        {
            if (pr.photonView.IsMine)
                playerRep = pr;
        }
        SetLocked(playerRep.IsVacuumLocked);
    }
    public override void DoAction()
    {
        
        activeTime = 15f;

        isActive = !isActive;

        drainOverTime = isActive;

        if (isActive)
        {
            audioSource.PlayOneShot(vacuumON);
            FindObjectOfType<GlobalAchievements>().GetAchievement("cleaning").CurrentCount++; // increase achievement
        }
        else
            audioSource.PlayOneShot(vacuumOFF);
    }
    private void OnTriggerEnter(Collider other)
    {
        VacuumTrash(other);
 
    }
    private void OnTriggerStay(Collider other)
    {
        VacuumTrash(other);
    }
    private void VacuumTrash(Collider other)
    {
        Debug.Log($"is active {isActive} | ");
        if (!isActive) return;
        if (other.gameObject.TryGetComponent(out Trash trash)) // check if trash is inside collider
        {
            if (trash.collected) return;
            if (player.TryGetComponent(out TrashBag bag))// check if player has trash bag
            {
                if (!bag.CanCollect())
                {
                    Debug.Log($"can collect| ");
                    if (isActive)
                        DoAction();
                    return;
                }
                trash.collected = true;
                StartCoroutine(VacuumTrash(trash, bag));
            }
        }
    }
   
    private IEnumerator VacuumTrash(Trash trash, TrashBag bag) // start vacuuming the trash
    {
        Transform camerTransform = player.transform.Find("Camera").transform;

        while (true) // move the trash towards the player 
        {
            float trashDistance = Vector3.Distance(trash.transform.position, camerTransform.position);
            if (trashDistance > 2f)
            {
                trash.transform.position = Vector3.MoveTowards(trash.transform.position, camerTransform.position, 1);
                yield return new WaitForSeconds(0.1f);
            }
            else
                break;
        }
        bag.AddTrash(trash); // add trash in the bag
    }

}
