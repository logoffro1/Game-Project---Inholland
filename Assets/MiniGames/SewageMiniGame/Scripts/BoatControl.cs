using UnityEngine;

public class BoatControl : MonoBehaviour
{
    private float speed = 1f;
    private float movementRange = 1.2f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!SewageMiniGame.Instance.IsPlaying) return;
            Drive();
    }
    private void Drive() // control the boat only on the horizontal axis
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(Vector3.right * horizontal);

        if (horizontal < 0)
            spriteRenderer.flipX = false;
        else if (horizontal > 0)
            spriteRenderer.flipX = true;

        if (transform.position.x > movementRange)
            transform.position = new Vector3(movementRange, transform.position.y, transform.position.z);
        else if (transform.position.x < -movementRange)
            transform.position = new Vector3(-movementRange, transform.position.y, transform.position.z);
    }

}
