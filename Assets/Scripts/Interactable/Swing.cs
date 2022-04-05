using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : InteractableObject
{
    private Animator anim;
    private void Start()
    {
        hoverName = "Push";
        anim = GetComponent<Animator>();
    }
    public override void DoAction(GameObject player)
    {
        anim.SetTrigger("PlayerPush");
    }
}
