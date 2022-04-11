using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float gravity = -9.81f;
    private float groundDistance = 0.4f;  

    private Vector3 velocity;
    private CharacterController controller;

    [SerializeField]
    private Transform groundCheck;
    public LayerMask groundMask;
    public bool IsGrounded { get; private set; }

    public bool canMove { get; set; } = true;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        canMove = true;
    }

    void Update()
    {
        if (MiniGameManager.Instance != null)
        {

            if (MiniGameManager.Instance.IsPlaying)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }

        if (!canMove) return;

        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Movement();
        Fall();

    }
    private void Fall()
    {
        if (IsGrounded && velocity.y < 0f)
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
