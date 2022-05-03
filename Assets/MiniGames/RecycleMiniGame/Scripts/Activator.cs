using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
        {
            if (notes.Count > 0)
            {
                GameObject currentNode = notes[0];
                var bin = bins.GetComponentsInChildren<RecycleBin>().Where(x => x.type == currentNode.GetComponent<Note>().type).FirstOrDefault();

                if (Input.GetKeyDown(bin.key))
                {
                    var noteComp = currentNode.GetComponent<Note>();
                    noteComp.Bin = bin;
                    game.CollectANote(noteComp.type);
                    game.ui.ExplodeParticles(currentNode);
                    StartCoroutine(WaitForDestroy(currentNode));
                }
                else
                {
                    game.RemoveALife();
                    Destroy(currentNode);
                }
            }
            else
            {
                game.RemoveALife();
            }
        }
    }

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
