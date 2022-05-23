using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using System;

// all objects that are interactable inherit from this class
public abstract class InteractableObject : MonoBehaviour
{
    protected string hoverName; // text to show while hovering over object

    public abstract void DoAction(GameObject player); // action to do after interacting
    public string GetHoverName() => hoverName; // set the text when hovering
    public bool IsInteractable = true;

    //localized string
    [SerializeField] protected LocalizeStringEvent localizedStringEvent;
    [SerializeField] protected LocalizedString localizedString;

    private LocalizationSettings locSettings;

    //Sets for the translation
    public async void SetLocalizedString(LocalizeStringEvent localizedStringEvent)
    {
        if (localizedStringEvent == null)
        {
            Debug.Log(this.GetType().ToString());
        }
        try
        {
            // get localization settings
            var handle = LocalizationSettings.InitializationOperation;
            await handle.Task;
            locSettings = handle.Result;

            // set the hover text
            this.localizedStringEvent = localizedStringEvent;
            this.hoverName = locSettings.GetStringDatabase().GetLocalizedString(localizedString.TableReference, localizedString.TableEntryReference);

            this.localizedStringEvent.StringReference = localizedString;
            this.localizedStringEvent.OnUpdateString.AddListener(OnStringChanged);
                
        }
        catch (Exception ex) //it gets here if localizedString is not set
        {
            Debug.Log(ex.ToString());
        }
    }
    protected virtual void OnStringChanged(string s) // change hover text
    {
        if (locSettings == null) return;
        this.hoverName = locSettings.GetStringDatabase().GetLocalizedString(localizedString.TableReference, localizedString.TableEntryReference);
    }
}
