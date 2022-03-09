using UnityEngine;

public class SewageDrain : MonoBehaviour, IInteractableObject
{
    public void DoAction(GameObject player)
    {
        Debug.Log("interacted");

    }

    public string GetHoverName() => "Sewage";
}
