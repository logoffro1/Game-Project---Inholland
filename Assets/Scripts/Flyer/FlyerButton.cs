using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlyerButton : MonoBehaviour
{

    public bool IsTitle;
    private TextMeshProUGUI title;
    private TextMeshProUGUI body;

    private FlyerMaking flyerMaking;
    public string Text;
    public float Points;

    private void Start()
    {
        flyerMaking = transform.parent.parent.GetComponent<FlyerMaking>();
        title = flyerMaking.Title;
        body = flyerMaking.Body;
    }

    public void ClickedMe()
    {
        string text = GetComponentInChildren<TextMeshProUGUI>().text;

        if (IsTitle)
        {
            title.text = text;
            flyerMaking.EnableButtons(flyerMaking.TitleButtons, false);
        }
        else
        {
            body.text = text;
            flyerMaking.EnableButtons(flyerMaking.BodyButtons, false);
        }

        flyerMaking.PressedAButton(Points);
        flyerMaking.PointForCurrentFlyer += Points;
        if (flyerMaking.AmountAccessed >= 2)
        {
            flyerMaking.MadeNewFlyer();
        }
    }

    public void SetButton(string text, float points)
    {
        this.Text = text;
        GetComponentInChildren<TextMeshProUGUI>().text = text;
        this.Points = points;
    }


}
