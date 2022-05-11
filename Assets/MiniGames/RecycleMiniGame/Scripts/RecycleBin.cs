using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    public NoteTypeEnum type;
    public KeyCode key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Note>().type == type)
        {
            Destroy(collision.gameObject);
        }
    }
}
