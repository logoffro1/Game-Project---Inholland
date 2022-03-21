using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Button : MonoBehaviour
{
    public Action<GameObject> onClick = delegate { };

    private void OnMouseDown()
    {
        onClick(gameObject);
    }

}
