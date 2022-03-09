using UnityEngine;
public interface IInteractableObject
{

    void DoAction(GameObject player);
    string GetHoverName();
}
