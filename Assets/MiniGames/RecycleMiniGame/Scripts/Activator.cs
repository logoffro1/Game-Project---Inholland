using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//The box that collides with the trash items
public class Activator : MonoBehaviour
{
    private List<GameObject> notes;
    public GameObject bins;
    private RecycleMiniGame game;

    void Start()
    {
        notes = new List<GameObject>();
        game = GetComponentInParent<RecycleMiniGame>();
    }

    private void Update()
    {
        //if any of teh correct keys are pressed
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
        {
            //if there still notes left
            if (notes.Count > 0)
            {
                GameObject currentNode = notes[0]; ;
                RecycleBin bin;
                try
                {
                    bin = bins.GetComponentsInChildren<RecycleBin>().Where(x => x.type == currentNode.GetComponent<Note>().type).FirstOrDefault();
                }
                catch
                {
                    //if the gameobject is gone, but key is pressed, lose a life
                    game.RemoveALife();
                    Destroy(currentNode);
                    return;
                }

                if (Input.GetKeyDown(bin.key))
                {
                    var noteComp = currentNode.GetComponent<Note>();
                    noteComp.Bin = bin;
                    game.CollectANote(noteComp.type);
                    game.ui.ConvertToWonDesign(currentNode);
                    StartCoroutine(WaitForDestroy(currentNode));
                }
                else
                {
                    //if the wrong key was pressed, lose a life
                    game.RemoveALife();
                    Destroy(currentNode);
                }
            }
        }
    }

    //Add collision, so it can check with this when key is pressed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        notes.Add(collision.gameObject);
    }

    public void RemoveFirstNote()
    {
        if (notes.Count > 0) notes.RemoveAt(0);
    }

    private IEnumerator WaitForDestroy(GameObject currentNode)
    {
        yield return new WaitForSeconds(5f);
        Destroy(currentNode);
    }
}
