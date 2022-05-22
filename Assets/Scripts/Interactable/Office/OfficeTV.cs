using UnityEngine;
using UnityEngine.Localization.Components;

public class OfficeTV : InteractableObject
{
    private OfficeLevelSelector officeLevels;

    [SerializeField] private LocalizeStringEvent localizeStringEvent;


    private void Start()
    {
        IsInteractable = true;
        officeLevels = GameObject.FindObjectOfType<OfficeLevelSelector>();
        SetLocalizedString(localizeStringEvent); // set hover text
    }

    public override void DoAction(GameObject player) // show level selection
    {
        officeLevels.ShowPanel(true);
        GameObject.FindObjectOfType<MouseLook>().canRotate = false;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = false;
        UIManager.Instance.ChangeCanvasShown();

    }
}
