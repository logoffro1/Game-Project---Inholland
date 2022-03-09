using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{

    public Color color;

    public int veryDarkMultiplier = 6;
    public Color veryDarkPreset = new Color(0f, 0f, 0f);
    public int darkMultiplier = 3;
    public Color darkPreset = new Color(0.1f, 0.1f, 0.1f);
    public Color mediumPreset = new Color(0.4f, 0.4f, 0.4f);
    public Color lightPreset = new Color(0.8f, 0.8f, 0.8f);

    // Start is called before the first frame update
    void Start()
    {

        foreach(Transform child in transform)
        {
            var childSprite = child.GetComponent<SpriteRenderer>();

            Color tmpColor = color;

            if (childSprite != null)
            {
                childSprite.color = color;

                if (childSprite.tag == "WireBackgroundPoint")
                {
                    tmpColor = (tmpColor / darkMultiplier) + darkPreset;
                }
                else if (childSprite.tag == "WireTop")
                {
                    tmpColor = tmpColor + mediumPreset;
                }

                tmpColor.a = 1;
                childSprite.color = tmpColor;

            }
            else
            {
                foreach (Transform child2 in child)
                {
                    tmpColor = color;

                    var childSprite2 = child2.GetComponent<SpriteRenderer>();

                    if (childSprite2.tag == "WireMiddle")
                    {
                        tmpColor = (tmpColor / veryDarkMultiplier) + veryDarkPreset;
                    }
                    else if (childSprite2.tag == "WireEnd")
                    {
                        tmpColor = tmpColor + lightPreset;
                    }

                    tmpColor.a = 1;
                    childSprite2.color = tmpColor;

                }

            }
        }
    }

}
