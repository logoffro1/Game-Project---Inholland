using UnityEngine.Localization.Components;
using UnityEngine;

//Script that goes on printer model, such that player can interact with it
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

    //Shows choice panel if clicked
    public override void DoAction(GameObject player)
    {
        ShowPanel(true);
    }

    //Shows the panel from the editor
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

            //If already printed, don't let them print more
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

    //Sets the correct panel if player goes back to office
    public void ReturnToOpenWorld()
    {
        ChoicePanel.SetActive(false);
        NoPanel.SetActive(false);
        PrinterPanel.SetActive(false);
        ProgressPanel.SetActive(false);

        ShowPanel(false);
    }

    //Sets the correct panel if player continues with 
    public void ChooseToContinue()
    {
        ChoicePanel.SetActive(false);
        PrinterCanvas.GetComponent<FlyerMaking>().Printer = this;
        PrinterPanel.SetActive(true);
    }


}
