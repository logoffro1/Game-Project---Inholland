
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
public abstract class InteractableObject : MonoBehaviour
{
    protected string hoverName;

    public abstract void DoAction(GameObject player);
    public string GetHoverName() => hoverName;
    public bool IsInteractable = true;

    //localized string
    [SerializeField] protected LocalizeStringEvent localizedStringEvent;
    [SerializeField] protected LocalizedString localizedString;

    private LocalizationSettings locSettings;
    public async void SetLocalizedString(LocalizeStringEvent localizedStringEvent)
    {
        try
        {
            var handle = LocalizationSettings.InitializationOperation;
            await handle.Task;
            locSettings = handle.Result;

            this.localizedStringEvent = localizedStringEvent;
            this.hoverName = locSettings.GetStringDatabase().GetLocalizedString(localizedString.TableReference, localizedString.TableEntryReference);

            this.localizedStringEvent.StringReference = localizedString;
            this.localizedStringEvent.OnUpdateString.AddListener(OnStringChanged);
                
        }
        catch (Exception ex) //it gets here if localizedString is not set
        {
            
            //Debug.Log(ex.ToString());
        }
    }

    protected virtual void OnStringChanged(string s)
    {
        if (locSettings == null) return;
        this.hoverName = locSettings.GetStringDatabase().GetLocalizedString(localizedString.TableReference, localizedString.TableEntryReference);
    }
}
