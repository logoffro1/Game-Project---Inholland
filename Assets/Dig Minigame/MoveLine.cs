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
    public GameObject heart;

    public DiggingMiniGame diggingMiniGame;
    // Start is called before the first frame update

    //speed
    private float startingSpeed = 2f;
    public float StartingSpeed { get { return startingSpeed;  } private set { startingSpeed = value; } }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    void Start()
    {
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
                gameOver = true;
                diggingMiniGame.GameFinish(true);
            }
            else
            {
                lives--;
                heart = GameObject.Find("Lives");
                Destroy(heart);
                
                if (lives == 0)
                {
                    gameOver = true;
                    diggingMiniGame.GameFinish(false);
                }
            }
        }
        if (movingRight == false)
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
            if (transform.localPosition.x <= leftLimitX)
            {
                movingRight = true;
                Debug.Log(movingRight);
            }
        }
        else
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
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
