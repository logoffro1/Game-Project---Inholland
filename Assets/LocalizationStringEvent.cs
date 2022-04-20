using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using System.Threading;

public class LocalizationStringEvent : MonoBehaviour
{



    public LocalizeStringEvent localizedStringEvent;
    LocalizedString originalLocalizedString;

    // Various ways to address a string table entry
    public LocalizedString localizedString = new LocalizedString { TableReference = "uiText", TableEntryReference = "interact.hover.text" };
    public string tableName = "uiText";
    public string keyName = "interact.hover.text";

    void Start()
    {
        // Keep track of the original so we dont change localizedString by mistake
        originalLocalizedString = localizedStringEvent.StringReference;


        // We can add a listener if we are interested in the Localized String.
        localizedStringEvent.OnUpdateString.AddListener(OnStringChanged);
    }

    void OnStringChanged(string s)
    {
        Debug.Log($"String changed to `{s}`");
        Thread.Sleep(5000);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Restore the original LocalizedString in case we changed it previously.
            localizedStringEvent.StringReference = originalLocalizedString;

            // Assign a new Table and Entry. This will trigger an update.
            localizedStringEvent.StringReference.SetReference(tableName, keyName);

            // We could do this if we only wanted to change the entry but use the same table
            // localizedStringEvent.StringReference.TableEntryReference = keyName;
        }
    }

}
