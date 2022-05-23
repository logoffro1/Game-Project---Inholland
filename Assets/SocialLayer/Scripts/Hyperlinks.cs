using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlinks : MonoBehaviour
{
    //This is used for opening the discord. there is a second part for adding reddit as well in case its needed.
    public string discordLink;
    public string redditLink;
   public void OpenUrl(string link)
    {
        Application.OpenURL(link);
    }
}
