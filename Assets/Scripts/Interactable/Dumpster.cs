using UnityEngine;

public class Dumpster : InteractableObject
{
    private void Start()
    {
        SetLocalizedString(this.localizedStringEvent);
    }
    public override void DoAction(GameObject player) // throw the trash away
    {
        if (player.transform.parent.TryGetComponent(out TrashBag trashBag))
        {
            trashBag.EmptyBag();
        }
    }
}
