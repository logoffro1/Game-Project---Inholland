using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    bool gameOver;
    bool movingRight = false;
    double leftLimitX = -0.5f;
    double rightLimitX = 0.5f;
    bool isOnTarget = false;
    int lives = 3;
    int timesDug;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public GameObject heart;
    public AudioClip heartLoss;
    public AudioClip success;
    public AudioSource audioSource;

    public DiggingMiniGame diggingMiniGame;
    // Start is called before the first frame update

    //speed
    private float startingSpeed = 2f;
    public float StartingSpeed { get { return startingSpeed;  } private set { startingSpeed = value; } }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        timesDug = 0;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
