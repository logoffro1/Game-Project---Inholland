using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Color color;

    private int darkMultiplier = 3;
    private Color darkPreset = new Color(0.1f, 0.1f, 0.1f);
    private Color mediumPreset = new Color(0.4f, 0.4f, 0.4f);

    void Start()
    {
        //Get the first child (wire -> child)
        foreach(Transform child in transform)
        {
            if (child.TryGetComponent(out SpriteRenderer childSprite))
            {
                Color tmpColor = color;

                if (childSprite != null)
                {
                    childSprite.color = color;

                    if (childSprite.CompareTag("WireBackgroundPoint"))
                    {
                        tmpColor = (tmpColor / darkMultiplier) + darkPreset;
                    }
                    else if (childSprite.CompareTag("WireTop"))
                    {
                        tmpColor = tmpColor + mediumPreset;
                    }

                    tmpColor.a = 1;
                    childSprite.color = tmpColor;
                }
            }
            else
            {
                //Get the second child (wire -> child -> child)
                foreach (Transform childOfChild in child)
                {
                    Color tmpColor = color;

                    if (childOfChild.TryGetComponent(out SpriteRenderer childSpriteOfChildSprite))
                    {
                        if (childSpriteOfChildSprite.CompareTag("WireEnd"))
                        {
                            tmpColor = tmpColor + mediumPreset;
                        }

                        tmpColor.a = 1;
                        childSpriteOfChildSprite.color = tmpColor;
                    }
                }
            }
        }
    }
}
