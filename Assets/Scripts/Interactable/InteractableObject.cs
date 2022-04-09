
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
    public LocalizeStringEvent localizedStringEvent;
    [SerializeField] protected LocalizedString localizedString;

    protected void SetLocalizedString()
    {
        this.hoverName = LocalizationSettings.StringDatabase.GetLocalizedString(localizedString.TableReference, localizedString.TableEntryReference);
        
        this.localizedStringEvent.StringReference = localizedString;
        this.localizedStringEvent.OnUpdateString.AddListener(OnStringChanged);
    }
    
    protected virtual void OnStringChanged(string s)
    {
        hoverName = localizedStringEvent.StringReference.GetLocalizedString();
    }
}
