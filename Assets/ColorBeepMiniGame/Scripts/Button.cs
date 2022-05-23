using UnityEngine;
using System;
public class Button : MonoBehaviour // color beep button
{
    public Action<GameObject> onClick = delegate { };

    private void OnMouseDown()
    {
        onClick(gameObject);
    }

}
