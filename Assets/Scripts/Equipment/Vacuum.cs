using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : Equipment
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip vacuumON;
    [SerializeField] private AudioClip vacuumOFF;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        drainOverTime = false;
        isActive = false;
        equipmentName = "Vacuum";
        activeTime = 20;
        maxCooldown = 5f;
        cooldown = maxCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (cooldown <= 0)
                DoAction();
        }
        if (!isActive)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                    cooldown = 0;
            }
        }
        DrainTime();



    }
    public override void DoAction()
    {
        if (isActive) cooldown = maxCooldown - activeTime;
        activeTime = 15f;

        isActive = !isActive;

        drainOverTime = isActive;

        if (isActive)
            audioSource.PlayOneShot(vacuumON);
        else
            audioSource.PlayOneShot(vacuumOFF);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (!isActive) return;
        if (other.gameObject.TryGetComponent(out Trash trash))
        {
            if (transform.parent.TryGetComponent(out TrashBag bag))
            {
                if (!bag.CanCollect()) return;
                StartCoroutine(VacuumTrash(trash, bag));
            }
            Debug.Log("TRASH IN RANGE");
        }
    }
    private IEnumerator VacuumTrash(Trash trash, TrashBag bag)
    {
        Transform camerTransform = transform.parent.Find("Camera").transform;

        while (true)
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
        bag.AddTrash(trash);
    }

}
