using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    //declaring variables
    bool gameOver;
    bool movingRight = false;
    //limits of the shovel move area
    double leftLimitX = -0.5f;
    double rightLimitX = 0.5f;
    //changed to true when the shovel is over the area where you can dig
    bool isOnTarget = false;
    //live count
    int lives = 3;
    //counts the amount of times you've dug to change the model of the shovel
    int timesDug;
    //model change array
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    //heart model
    public GameObject heart;
    //audio clips for losing hearts and success, to be assigned in the unity inspector
    public AudioClip heartLoss;
    public AudioClip success;
    public AudioSource audioSource;

    public DiggingMiniGame diggingMiniGame;
    // Start is called before the first frame update

    //initial speed of the shovel to be determined by the dynamic difficulty of the level
    private float startingSpeed = 2f;
    public float StartingSpeed { get { return startingSpeed;  } private set { startingSpeed = value; } }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    void Start()
    {
        //initializes the game
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        timesDug = 0;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the game is over
        if (gameOver) { return; }
        //checks when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if space is pressed and the shovel is on target,
            //a success sound is played, the times dug is incremented,
            //and the model for the shovel is changed
            //a check if this is the third time digging is also done, and if it is
            //the game ends and results in a win
            if (isOnTarget)
            {
                audioSource.PlayOneShot(success);
                timesDug++;
                spriteRenderer.sprite = spriteArray[timesDug];
                spriteRenderer.sortingOrder = 3;
                if (timesDug == 3)
                {
                    audioSource.PlayOneShot(success);
                    gameOver = true;
                    diggingMiniGame.GameFinish(true);
                }
            }
            //if space is pressed and the shovel is not on target,
            //lives are reduced, the heart sprite is destroyed and
            //an audio prompt is played to show the loss of the heart
            //lives is also checked to see if they are 0, and if they are
            //the game ends and results in a loss
            else
            {
                lives--;
                heart = GameObject.Find("Lives");
                Destroy(heart);
                audioSource.PlayOneShot(heartLoss);

                if (lives == 0)
                {
                    gameOver = true;
                    diggingMiniGame.GameFinish(false);
                }
            }
        }
        //checks the direction of the shovel, and turns it around once it reaches the limits specified in the variable,
        //if it is not moving right then it has to be moving left, and there is a check to see if it has reached either limit
        if (movingRight == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.localPosition.x <= leftLimitX)
            {
                movingRight = true;
                //Debug.Log(movingRight);
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.localPosition.x >= rightLimitX)
            {
                movingRight = false;
            }
        }

    }

    //collider checks to see if it is on target, to determine whether to deduct a life or change the shovel model
   private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        isOnTarget = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        isOnTarget = false;
    }
}
