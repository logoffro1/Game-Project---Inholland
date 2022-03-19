using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    bool gameOver;
    bool movingRight = false;
    double leftLimitX = -0.495f;
    double rightLimitX = 0.495f;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
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
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Victory!");
        }
    }
}
