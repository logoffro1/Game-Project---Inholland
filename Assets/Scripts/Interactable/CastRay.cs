using UnityEngine;

public class CastRay : MonoBehaviour
{
    [SerializeField]
    private float maxObjectDistance = 3f;

    private static CastRay _instance = null;
    public static CastRay Instance { get { return _instance; } }
    private GameObject objectHit;


    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Update()
    {
        GetObjectHit();
    }
    private void GetObjectHit() //THIS IS TEMP CODE / IMPROVE LATER
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * maxObjectDistance, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxObjectDistance, out hit, maxObjectDistance))
        {
            GameObject obj = hit.transform.gameObject; 
            objectHit = obj.GetComponent<InteractableObject>() == null ? null : hit.transform.gameObject;
            InteractableObject interactableObject = objectHit?.GetComponent<InteractableObject>();

            if (objectHit != null && interactableObject != null && interactableObject.IsInteractable)
            {
                UIManager.Instance.SetHoverText(interactableObject.GetHoverName());
                if (Input.GetKeyDown(KeyCode.E))
                    interactableObject.DoAction(gameObject);

                return;
            }         
        }
        UIManager.Instance.SetHoverText(null);
    }
}
