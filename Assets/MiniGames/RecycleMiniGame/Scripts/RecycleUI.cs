using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleUI : MonoBehaviour
{
    public List<GameObject> lives;

    public void RemoveAHeart()
    {
        if (lives.Count - 1 < 0) return;

        GameObject life = lives[lives.Count - 1];
        Destroy(life);
        lives.Remove(life);
        //TODO: Add animation
    }

    public void MoveNoteToBin(GameObject note, Vector3 position)
    {
        //note.transform.LookAt(position);
    }
}
