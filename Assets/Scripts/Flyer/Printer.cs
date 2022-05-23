using UnityEngine.Localization.Components;
using UnityEngine;

public class Printer : InteractableObject
{
    [SerializeField] private LocalizeStringEvent localizeStringEvent;

    public GameObject PrinterCanvas;
    public GameObject ChoicePanel;
    public GameObject NoPanel;
    public GameObject PrinterPanel;
    public GameObject ProgressPanel;

    private void Start()
    {
        IsInteractable = true;
        SetLocalizedString(localizeStringEvent);
    }

    public override void DoAction(GameObject player)
    {
        ShowPanel(true);
    }

    private void ShowPanel(bool show)
    {
        CastRay.Instance.CanInteract = !show;

        PrinterCanvas.SetActive(show);

        Cursor.visible = show;

        GameObject.FindObjectOfType<MouseLook>().canRotate = !show;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = !show;

        UIManager.Instance.TurnOnCanvas(!show);

        if (show)
        {
            Cursor.lockState = CursorLockMode.None;

            if (PrinterCanvas.GetComponent<FlyerMaking>().IsDoneForToday)
            {
                NoPanel.SetActive(true);
            }
            else
            {
                ChoicePanel.SetActive(true);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void ReturnToOpenWorld()
    {
        ChoicePanel.SetActive(false);
        NoPanel.SetActive(false);
        PrinterPanel.SetActive(false);
        ProgressPanel.SetActive(false);

        ShowPanel(false);
    }

    public void ChooseToContinue()
    {
        ChoicePanel.SetActive(false);
        PrinterCanvas.GetComponent<FlyerMaking>().Printer = this;
        PrinterPanel.SetActive(true);
    }


}
