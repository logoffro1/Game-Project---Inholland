using UnityEngine;
using System;
public class Collectible : MonoBehaviour
{
    public Action onCollect = delegate { };
    private float originalSpeed = .35f;
    public float OriginalSpeed { get { return originalSpeed; } set { originalSpeed = value; } }
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    private float rotationSpeed;
    bool attached = false;
    public AudioClip passedClip; //if the collectible goes out of bounds
    public LayerMask boatMask;
    private AudioSource audioSource;

    private bool isDestroyed = false;
    private void Awake()
    {
        
        onCollect += Collect;
        rotationSpeed = UnityEngine.Random.Range(5f, 30f); 
        audioSource = GetComponent<AudioSource>();
        speed = originalSpeed;

    }
    void Update()
    {
        if (!SewageMiniGame.Instance.IsPlaying) return;

        if (!attached)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime,Space.World);
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }


        if (transform.localPosition.x <= -1.7f)
            OutOfBounds();

        if (Physics2D.OverlapCircle(transform.position, 0.005f, boatMask))
        {
            PickUp();
        }
          
    }
    private void Collect()
    {
        SewageMiniGame.Instance.IncreaseScore();
        transform.localPosition = Vector2.zero;
        attached = true;
    }
    private void PickUp()
    {
        Destroy(gameObject, 0.2f);
    }
    private void OutOfBounds()
    {   if (isDestroyed) return;

        isDestroyed = true;
        SewageMiniGame.Instance.DecreaseLives();
        audioSource.PlayOneShot(passedClip);
        Destroy(gameObject,0.5f);
    }
}
