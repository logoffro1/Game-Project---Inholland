using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterWobble : MonoBehaviour
{
    TMP_Text textMesh;
    float originalFontSize;

    public float speedMultiplier = 1;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        originalFontSize = textMesh.fontSize;
    }

    //Putting the beat
    void Update()
    {
        textMesh.fontSize = originalFontSize * Beat(Time.time * speedMultiplier);
    }

    //The math behind the beat
    float Beat(float time)
    {
        return Mathf.Cos(10 * time) * 0.1f + 1f;
    }
}
