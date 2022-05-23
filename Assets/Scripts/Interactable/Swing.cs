using UnityEngine;

public class Swing : InteractableObject
{
    private Animator anim;
    private void Start()
    {
        hoverName = "Push";
        anim = GetComponent<Animator>();
    }
    public override void DoAction(GameObject player) // set swing anim
    {
        anim.SetTrigger("PlayerPush");
    }
}
