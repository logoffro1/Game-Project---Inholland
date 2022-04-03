using UnityEngine;

public class CastRay : MonoBehaviour
{
    [SerializeField]
    private float maxObjectDistance = 4f;

    private static CastRay _instance = null;
    public static CastRay Instance { get { return _instance; } }
    private GameObject objectHit;
    private GameObject previousObject;

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
                if (previousObject != null)
                {
                    previousObject.layer = 0;
                    previousObject = null;
                }
                UIManager.Instance.SetHoverText(interactableObject.GetHoverName());
                if (Input.GetKeyDown(KeyCode.E))
                    interactableObject.DoAction(gameObject);

                if (objectHit == previousObject) return;
                if (objectHit.layer != 8) //outlined
                    objectHit.layer = 8;


                if (objectHit != null)
                {
                    previousObject = objectHit;
              
                }
                return;
            }         
        }
        if (previousObject != null)
        {
            previousObject.layer = 0;
            previousObject = null;
        }
        UIManager.Instance.SetHoverText(null);
    }
}

