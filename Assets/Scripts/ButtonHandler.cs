using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void LoadOfficeScene()
    {
        LevelManager.Instance.LoadScene("Office",false);
    }
}
