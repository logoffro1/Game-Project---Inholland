using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SolarPanel : MonoBehaviour, IInteractableObject
{
    public void DoAction(GameObject player)
    {
        SceneManager.LoadScene("Rewire");
    }

    public string GetHoverName() => "Solar Panel";
}
