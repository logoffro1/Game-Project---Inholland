using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawn the notes
public class NoteSpawner : MonoBehaviour
{
    public GameObject NoteContainer;
    private Vector3 spawnPosition;
    private bool gameFinished = false;
    public float Speed = 0.05f;
    public float MinWaitTime = 5f;
    public float MaxWaitTime = 6f;

    private List<GameObject> notePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position + new Vector3(0, 1, 0);

        notePrefabs = new List<GameObject>();
        foreach(Transform child in NoteContainer.transform)
        {
            notePrefabs.Add(child.gameObject);
        }

        StartCoroutine(SpawnNotes());
    }

    private IEnumerator SpawnNotes()
    {
        yield return new WaitForSeconds(2f);

        while (!gameFinished)
        {
            //Instianciate the trash
            GameObject note = Instantiate(notePrefabs[Random.Range(0, notePrefabs.Count)], spawnPosition, transform.rotation, transform);

            //Sets the correct background color to each item
            Color color;
            switch(note.GetComponent<Note>().type)
            {
                case NoteTypeEnum.Paper:
                    color = new Color(0.75f, 0.75f, 0.4f, 0.6f);
                    break;
                case NoteTypeEnum.Plastic:
                    color = new Color(0.59f, 0.37f, 0.71f, 0.6f);
                    break;
                case NoteTypeEnum.Organic:
                    color = new Color(0.42f, 0.73f, 0.38f, 0.6f);
                    break;
                case NoteTypeEnum.Glass:
                    color = new Color(0.37f, 0.55f, 0.71f, 0.6f);
                    break;
                default:
                    color = Color.white;
                    break;
            }

            note.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));
        }
    }
}
