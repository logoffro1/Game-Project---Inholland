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

        switch (status)
        {
            case TaskStatus.Success:
                modelPrefab = SuccessPrefabModel;
                break;
            case TaskStatus.Fail:
                modelPrefab = FailPrefabModel;
                break;
            case TaskStatus.Touched:
                modelPrefab = TouchedPrefabModel;
                break;
            case TaskStatus.Untouched:
                modelPrefab = UntouchedPrefabModel;
                break;
        }


        GameObject gameObject = Instantiate(modelPrefab, transform.position, transform.rotation, transform);

        //If Touched or faUntouchedil, should add all of these things
        if (status == TaskStatus.Touched || status == TaskStatus.Untouched)
        {
            //Gets all necessary info
            InteractableTaskObject interactable = gameObject.GetComponent<InteractableTaskObject>();
            interactable.enabled = true;
            interactable.IsInteractable = true;
            interactable.Status = status;
            interactable.GamePrefab = gamePrefab;
        }
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
