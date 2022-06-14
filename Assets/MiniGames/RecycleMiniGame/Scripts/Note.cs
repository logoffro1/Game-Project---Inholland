using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Individual trash items
public class Note : MonoBehaviour
{
    Rigidbody2D rb;
    private float speed;
    public NoteTypeEnum type;
    public RecycleBin Bin;

    void Start()
    {
        speed = GetComponentInParent<NoteSpawner>().Speed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //goes down
        var step = Time.deltaTime * -speed;
        transform.position += transform.up * step;

        //if a bin is set, it will go towards the bin. Else, it continues straight down
        if (Bin == null)
        {
            transform.position += transform.up * step;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Bin.transform.position.x, Bin.transform.position.y, transform.position.z), -step);
        }
    }

}

//Different types of trash
public enum NoteTypeEnum
{
    Paper,
    Plastic,
    Organic,
    Glass
}
