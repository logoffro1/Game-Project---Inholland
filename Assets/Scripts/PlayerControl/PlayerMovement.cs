using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    public float Speed { get { return speed;  } set { speed = value; } }
    [SerializeField]
    private float gravity = -9.81f;
    private float groundDistance = 0.4f;  

    private Vector3 velocity;
    private CharacterController controller;

    [SerializeField]
    private Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;

    [SerializeField]
    private Animator anim;

    public bool canMove { get; set; } = true;
    void Start()
    {
        Time.timeScale = 1f;
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(!IsRunning(horizontal,vertical))
        {
            anim.SetBool("isRunning", false);
        }

        if (!canMove)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Movement(horizontal,vertical);
        Fall();

    }
    private void Fall()
    {
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Movement(float horizontal, float vertical)
    {
        if (IsRunning(horizontal,vertical))
        {
            anim.SetBool("isRunning", true);
        }
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
    }
    public bool IsRunning(float horizontal, float vertical) => horizontal != 0 || vertical != 0; 
}
