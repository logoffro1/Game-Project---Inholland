using UnityEngine;
public class Trash : InteractableObject // collectable trash
{
    public bool collected { get; set; } = false;
    private void Awake()
    {
        hoverName = "Trash";
    }
    private void Update()
    {
        if (transform.position.y < -50) // destroy if falls out of bounds
            Destroy(gameObject);
    }
    public override void DoAction(GameObject player) // if the player has trash bag, pick up trash
    {
        if (player.transform.parent.TryGetComponent(out TrashBag bag))
        {
            bag.AddTrash(this);
        }

    }
}
