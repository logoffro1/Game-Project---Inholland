
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected string hoverName;

    public abstract void DoAction(GameObject player);
    public string GetHoverName() => hoverName;
    public bool IsInteractable;
}
