using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    /* Notes from Cosmin
     * 1. Don't make variables public unless there is no other option but to assign from inspector
     *    If you want to see variables in the inspector for debugging, add [SerializeField] in front of a private field and it will show in the inspector
     *    Or for stuff that you want to use outside the class, use properties (get/ private set)
     * 2. If you're gonna use tags, use .CompareTag() instead of '==' to compare them, it's faster and less prone to errors
     * 3. Look into TryGetComponent<> - https://docs.unity3d.com/ScriptReference/Component.TryGetComponent.html
     * 4. you can use an array to store the Presets (or even dictionary if you wanna assign them a name), but that's mostly just so it looks prettier 
     */

    public Color color;

    private int darkMultiplier = 3;
    private Color darkPreset = new Color(0.1f, 0.1f, 0.1f);
    private Color mediumPreset = new Color(0.4f, 0.4f, 0.4f);

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

                    if (childSprite2.tag == "WireEnd")
                    {
                        tmpColor = tmpColor + mediumPreset;
                    }

                    tmpColor.a = 1;
                    childSprite2.color = tmpColor;

                }
            }
        }
    }

}
