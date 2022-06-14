using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Square under activator, it will destroy the item if it collides and will remove a life
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
