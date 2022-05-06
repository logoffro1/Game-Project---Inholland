using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviourPun
{
    [SerializeField]
    private float sensitivity = 200f;
    private float xRotation = 0f;

    [SerializeField]
    private Transform player;

    public bool canRotate { get; set; } = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canRotate = true;
    }

    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        if (MiniGameManager.Instance != null)
        {

            if (MiniGameManager.Instance.IsPlaying)
            {
                canRotate = false;
            }
            else
            {
                canRotate = true;
            }
        }
        if (!canRotate) return;
        LookAround();


    }
    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
