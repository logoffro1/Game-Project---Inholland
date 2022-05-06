using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SceneViewCameraProxy : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneView SceneView;
    public Camera Camera;

    public Camera ReferenceCamera;
    public bool UseReferenceCamera;

    public void OnEnable()
    {
        Camera = GetCamera();
        UpdateComponents();
    }

    private Camera GetCamera()
    {
        SceneView = EditorWindow.GetWindow<SceneView>();
        return SceneView.camera;
    }

  /*  private Component[] GetComponents()
    {
        var result = UseReferenceCamera
            ? ReferenceCamera.GetComponents<Component>()
            : GetComponents<Component>();

        if (result != null && result.Length > 1) // Exclude Transform
        {
            result = result.Except(new[] { UseReferenceCamera ? ReferenceCamera.transform : transform }).ToArray();

            var hasCamera = UseReferenceCamera ? true : GetComponent<Camera>() != null;
            if (hasCamera)
                result = UseReferenceCamera ? result.Except(new[] { ReferenceCamera }).ToArray() : result.Except(new[] { GetComponent<Camera>() }).ToArray();
        }

        return result;
    } */

    private void UpdateComponents()
    {
        if (Camera == null)
            Camera = GetCamera();

        if (Camera == null) // This shouldn't happen, but it does
            return;


            GameObject cameraGo = Camera.gameObject;

            var cType = GetComponent<HighSpeedPostProcessing>().GetType();

            var existing = cameraGo.GetComponent(cType) ?? cameraGo.AddComponent(cType);
            EditorUtility.CopySerialized(GetComponent<HighSpeedPostProcessing>(), existing);

        
    }
#endif
}