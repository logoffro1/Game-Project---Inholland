using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float gravity = -9.81f;
    private float groundDistance = 0.4f;  

    private Vector3 velocity;
    private CharacterController controller;

    [SerializeField]
    private Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (MiniGameManager.Instance.IsPlaying) return;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Movement();
        Fall();

    }
    private void Fall()
    {
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
    }
}
