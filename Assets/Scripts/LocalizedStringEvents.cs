using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

public class LocalizedStringEvents : MonoBehaviour
{
    public LocalizedString myString;

    string localizedText;

    /// <summary>
    /// Register a ChangeHandler. This is called whenever the string needs to be updated.
    /// </summary>
    void OnEnable()
    {
        myString.StringChanged += UpdateString;
    }

    void OnDisable()
    {
        myString.StringChanged -= UpdateString;
    }

    void UpdateString(string s)
    {
        localizedText = s;
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField(localizedText);
    }
}
