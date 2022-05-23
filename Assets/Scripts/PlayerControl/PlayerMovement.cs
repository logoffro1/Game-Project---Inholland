using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
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
    private Transform groundCheck; // to check if colliding with ground
    public LayerMask groundMask;
    private bool isGrounded;

    [SerializeField]
    private Animator anim;

    public bool canMove { get; set; } = true;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        canMove = true;
    }

    void Update()
    {
        if (!photonView.IsMine) return;
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
        
        // get movement axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(!IsRunning(horizontal,vertical)) // set running/idle anims
        {
            anim.SetBool("isRunning", false);
        }

        if (!canMove)
        {
            return;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // check if standing on the ground

        Movement(horizontal,vertical);
        Fall();


    }
    public void SpeedBoost(bool activate) // apply a speed boost
    {
        if (activate)
        {

            speed += 2.5f;
        }
        else
        {
            speed -= 2.5f;
        }
    }
    private void Fall() // let the player fall due to gravity
    {
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Movement(float horizontal, float vertical) // move the player with the controller and apply the animations
    {
        if (IsRunning(horizontal,vertical))
        {
            anim.SetBool("isRunning", true);
        }
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
    }
    public bool IsRunning(float horizontal, float vertical) => horizontal != 0 || vertical != 0; // check if running or standing
}
