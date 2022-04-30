using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureSquare : MonoBehaviour
{
    private RecycleMiniGame game;

    private void Start()
    {
        game = GetComponentInParent<RecycleMiniGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Note>().Bin == null)
        {
            Destroy(collision.gameObject);
            game.RemoveALife();
        }
    }
}
