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

    public DiggingMiniGame diggingMiniGame;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) { return; }
        if (Input.GetKeyDown(KeyCode.Space) && isOnTarget)
        {
            gameOver = true;
            diggingMiniGame.GameFinish(true);
        }
        if (movingRight == false)
        {
            transform.Translate(Vector2.left * 2f * Time.deltaTime);
            if (transform.localPosition.x <= leftLimitX)
            {
                movingRight = true;
                Debug.Log(movingRight);
            }
        }
        else
        {
            transform.Translate(Vector2.right * 2f * Time.deltaTime);
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
