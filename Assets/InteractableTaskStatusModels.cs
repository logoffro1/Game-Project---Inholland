using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTaskStatusModels : MonoBehaviour
{
    public GameObject UntouchedPrefabModel;
    public GameObject TouchedPrefabModel;
    public GameObject SuccessPrefabModel;
    public GameObject FailPrefabModel;

    private void InstantiateModel(TaskStatus status, GameObject gamePrefab)
    {
        GameObject modelPrefab = UntouchedPrefabModel;
        bool isStillInteractable = false;

        switch (status)
        {
            case TaskStatus.Success:
                modelPrefab = SuccessPrefabModel;
                isStillInteractable = false;
                break;
            case TaskStatus.Fail:
                modelPrefab = FailPrefabModel;
                isStillInteractable = false;
                break;
            case TaskStatus.Touched:
                modelPrefab = TouchedPrefabModel;
                isStillInteractable = true;
                break;
            case TaskStatus.Untouched:
                modelPrefab = UntouchedPrefabModel;
                isStillInteractable = true;
                break;
        }


        GameObject gameObject = Instantiate(modelPrefab, transform.position, transform.rotation, transform);

        //Gets all necessary info
        InteractableTaskObject interactable = gameObject.GetComponent<InteractableTaskObject>();
        interactable.enabled = isStillInteractable;
        interactable.IsInteractable = isStillInteractable;
        interactable.Status = status;

        if (isStillInteractable) interactable.GamePrefab = gamePrefab;
    }

    public void ChangeModel(TaskStatus status)
    {
        GameObject gamePrefab = transform.GetChild(0).gameObject.GetComponent<InteractableTaskObject>().GamePrefab;

        //Destroys old model
        Destroy(transform.GetChild(0).gameObject);

        //Instaiates new model
        InstantiateModel(status, gamePrefab);
    }
}
