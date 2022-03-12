using UnityEngine;

public class SewageDrain : MonoBehaviour, IInteractableObject
{
    public GameObject SewageGamePrefab;
    public void DoAction(GameObject player)
    {
        MiniGameManager.Instance.StartGame(SewageGamePrefab);
    }

    public string GetHoverName() => "Sewage";
}
