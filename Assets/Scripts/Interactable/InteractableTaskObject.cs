using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

//Inherits from InteractableObject
//This class is put onto the individual models, which the player will intract with to star ta minigame
public class InteractableTaskObject : InteractableObject
{
    //This class is in the interactable object in the container, thus not the container itself but the actual model 

    public GameObject GamePrefab;

    [HideInInspector]
    public int AmountTries = 0;


    [HideInInspector]
    public GameObject CurrentModel;

    //The container in which this model is in
    [HideInInspector]
    public InteractableTaskStatusModels interactableTaskStatusModels;

    //The status in which this model is
    public TaskStatus Status;

    private void Start()
    {
        //DetermineObject();
        SetInteractableTaskStatusModels();
    }

    //Sets to the correct InteractableTaskStatusModels
    private void SetInteractableTaskStatusModels()
    {
        if (gameObject.TryGetComponent(out InteractableTaskStatusModels childStatusModels))
        {
            interactableTaskStatusModels = childStatusModels;
        }
        else if (gameObject.transform.parent.gameObject.TryGetComponent(out InteractableTaskStatusModels statusModels))
        {
            interactableTaskStatusModels = statusModels;
        }
        else if (gameObject.transform.parent.gameObject.TryGetComponent(out InteractableTaskStatusModels parentstatusModels))
        {
            interactableTaskStatusModels = parentstatusModels;
        }
        else if (gameObject.transform.parent.parent.gameObject.TryGetComponent(out InteractableTaskStatusModels grandParentStatusModels))
        {
            interactableTaskStatusModels = grandParentStatusModels;
        }
    }

    //Changes the model of the interactableTaskStatusModels
    public GameObject ChangeModel(TaskStatus status)
    {
        if (interactableTaskStatusModels == null) SetInteractableTaskStatusModels();
        return interactableTaskStatusModels.ChangeModel(status);
    }

    //When clicked/interacted with, startthe game
    public override void DoAction(GameObject player)
    {
        //A check that the model is interactavle
        if (IsInteractable)
        {
            MiniGameManager.Instance.StartGame(GamePrefab);
            MiniGameManager.Instance.InteractableObject = this;

            //If it's interactavle and it was clicked, change it to touched
            if (Status != TaskStatus.Touched)
            {
                ChangeModel(TaskStatus.Touched);
            }

            AmountTries++;
        }
        else
        {
            hoverName = string.Empty;
        }
    }
}
