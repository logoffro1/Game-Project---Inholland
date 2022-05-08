using UnityEngine.Localization.Components;
using UnityEngine;

public class Printer : InteractableObject
{
    [SerializeField] private LocalizeStringEvent localizeStringEvent;

    private bool IsDoneForToday = false;
    private bool isPanelActive = false;
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
        isPanelActive = !isPanelActive;
        PrinterCanvas.SetActive(show);

        if (IsDoneForToday)
        {
            NoPanel.SetActive(true);
        }
        else
        {
            ChoicePanel.SetActive(true);
            IsDoneForToday = true;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = show;
        GameObject.FindObjectOfType<MouseLook>().canRotate = !show;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = !show;
        UIManager.Instance.ChangeCanvasShown();
    }

    public void ReturnToOpenWorld()
    {
        ChoicePanel.SetActive(false);
        NoPanel.SetActive(false);
        PrinterPanel.SetActive(false);
        ProgressPanel.SetActive(false);

        ShowPanel(false);
    }

    public void ChoseToContinue()
    {
        ChoicePanel.SetActive(false);
        PrinterPanel.GetComponent<FlyerMaking>().Printer = this;
        PrinterPanel.SetActive(true);
    }


}
