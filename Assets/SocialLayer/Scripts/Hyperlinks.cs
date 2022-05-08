using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlinks : MonoBehaviour
{
    public string discordLink;
    public string redditLink;
   public void OpenUrl(string link)
    {
        Application.OpenURL(link);
    }
}
