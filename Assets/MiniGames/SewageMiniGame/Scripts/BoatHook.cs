using System.Collections;
using UnityEngine;
using System;

// hook to collect trash with
public class BoatHook : MonoBehaviour
{

    private LineRenderer lr; // hook line
    private CircleCollider2D circleCollider;

    public GameObject HookOpenPrefab;
    public GameObject HookClosedPrefab;
    public AudioClip hookCollectSound;
    private AudioSource audioSource;
    private float speed = .7f;
    private float hookDistance = 0.7f; // total hook length
    private bool trashAttached = false;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        lr = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (!SewageMiniGame.Instance.IsPlaying) return;
        if (Input.GetKeyDown(KeyCode.Space)) { StartCoroutine(ShootHookDown()); }
    }
    private IEnumerator ShootHookDown() // send hook down
    {
        HookOpenPrefab.SetActive(true);
        HookClosedPrefab.SetActive(false);
        float startTime = Time.time;
        while (true) // smoothly increase the size of the hook line
        {
            if (trashAttached) break;

            float distCovered = (Time.time - startTime) * speed; // current

            lr.SetPosition(0, transform.position);
            Vector3 newPos = Vector3.Lerp(lr.GetPosition(0), lr.GetPosition(1) + new Vector3(0, -0.5f, 0), distCovered); // hook position

            Vector2 offsetPos = new Vector2(0, (lr.GetPosition(1).y - lr.GetPosition(0).y));
            circleCollider.offset = offsetPos; // collider to pick up trash
            HookOpenPrefab.transform.position = newPos;
            lr.SetPosition(1, newPos);
            if (distCovered >= hookDistance)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        trashAttached = true;
        StartCoroutine(RetractHook(null));
    }
    private IEnumerator RetractHook(GameObject item) // reverse the hook
    {
        HookClosedPrefab.SetActive(true);
        HookOpenPrefab.SetActive(false);
        circleCollider.offset = new Vector2(0, 0);
        float startTime = Time.time;
        while (true)
        {
            if (!trashAttached) break;

            float distCovered = (Time.time - startTime) * speed;
            lr.SetPosition(0, transform.position);
            Vector3 newPos = Vector3.Lerp(lr.GetPosition(1), lr.GetPosition(0), distCovered / 5);


            lr.SetPosition(1, newPos);
            if (item != null)
            {
                item.transform.position = newPos;
            }
            HookClosedPrefab.transform.position = newPos;

            if (Math.Round(lr.GetPosition(1).y,2) == Math.Round(lr.GetPosition(0).y,2))
                break;
            yield return new WaitForSeconds(0.01f);
        }
        trashAttached = false;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
        HookClosedPrefab.transform.position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if entered trash trigger, collect
        if (other.TryGetComponent(out Collectible collectible) && !trashAttached)
        {
            collectible.onCollect();
            Collect(collectible.gameObject);
        }
    }
    private void Collect(GameObject item) // collect trash and retract hook
    {
        audioSource.PlayOneShot(hookCollectSound);
        trashAttached = true;
        StartCoroutine(RetractHook(item));
    }
}
