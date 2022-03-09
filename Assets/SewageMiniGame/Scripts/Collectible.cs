using UnityEngine;
using System;
public class Collectible : MonoBehaviour
{
    public Action onCollect = delegate { };
    private float speed = .4f;
    private float rotationSpeed;
    bool attached = false;

    private GameObject go;
    public LayerMask boatMask;
    private void Awake()
    {
        onCollect += Collect;
        rotationSpeed = UnityEngine.Random.Range(5f, 30f); 
    }
    void Update()
    {
        if (!SewageMiniGame.Instance.isPlaying) return;

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
    {
        Debug.Log("OUT OF BOUNDS");
        SewageMiniGame.Instance.DecreaseLives();

        Destroy(gameObject);
    }
}
