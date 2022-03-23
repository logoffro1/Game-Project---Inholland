using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 200f;
    private float xRotation = 0f;

    [SerializeField]
    private Transform player;

    void Start()
    {
        Debug.Log(Cursor.lockState);
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log(Cursor.lockState);
    }

    void Update()
    {
        if (MiniGameManager.Instance.IsPlaying) return;
        LookAround();
        if (Input.GetKeyDown(KeyCode.Escape))
            LevelManager.Instance.LoadScene("MainMenu");
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
