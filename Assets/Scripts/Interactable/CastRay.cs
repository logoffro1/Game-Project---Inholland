using UnityEngine;

public class CastRay : MonoBehaviour
{
    [SerializeField]
    private float maxObjectDistance = 4f; // cast ray range

    private static CastRay _instance = null;
    public static CastRay Instance { get { return _instance; } }
    private GameObject objectHit; // looking at
    private GameObject previousObject; // looked at
    public bool CanInteract { get; set; } = true;

    private void Awake() //singleton
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    private void Start()
    {
        SetCustomRenderRange();
    }
    private void SetCustomRenderRange()
    {
        // change the culling distance to specific layers
        Camera cam = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[11] = 60; //xray
        distances[12] = 30; // dumpster
        cam.layerCullDistances = distances;
    }
    void Update()
    {
        ChangeRayDistance();
        GetObjectHit();
    }
    private void GetObjectHit()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * maxObjectDistance, Color.green);
        RaycastHit hit;

        // if raycast hit an object
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxObjectDistance, out hit, maxObjectDistance))
        {
            //get object hit
            GameObject obj = hit.transform.gameObject;
            objectHit = obj.GetComponent<InteractableObject>() == null ? null : hit.transform.gameObject;
            InteractableObject interactableObject = objectHit?.GetComponent<InteractableObject>();

            // check if object is interactable
            if (objectHit != null && interactableObject != null && interactableObject.IsInteractable)
            {
                if (!CanInteract) return;
                if (objectHit.layer == 11 || objectHit.layer == 12) return; //xray  & dumpster layer

                if (previousObject != null)
                {
                    previousObject.layer = 0;
                    previousObject = null;
                }
                UIManager.Instance.SetHoverText(interactableObject.GetHoverName());
                if (Input.GetKeyDown(KeyCode.E))
                    interactableObject.DoAction(gameObject);

                if (objectHit == previousObject) return;
                if (objectHit.layer != 8) // task outline
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
    private void ChangeRayDistance()
    {
        // increase ray distance while looking UP at an angle (for reaching solar panels)

        if (transform.localRotation.eulerAngles.x >= 270f && transform.localRotation.eulerAngles.x <= 325f)
            maxObjectDistance = 20f;
        else
            maxObjectDistance = 4f;
    }

}

