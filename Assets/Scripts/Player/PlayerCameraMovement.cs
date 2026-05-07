using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    public Transform playerCamera;
    public float Sensetivity;
    public float cameraSmoothness;
    private float targetXRotation;
    private float targetYaw;
    private float currentYaw;
    private Vector2 PlayerMouseInput;
    private float xRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        targetXRotation = xRotation;
        targetYaw = transform.eulerAngles.y;
        currentYaw = targetYaw;
    }
    private void Update()
    {
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MoveCamera();

    }
    private void MoveCamera()
    {
        targetXRotation -= PlayerMouseInput.y * Sensetivity;
        targetXRotation = Mathf.Clamp(targetXRotation, -90f, 90f);

        targetYaw += PlayerMouseInput.x * Sensetivity;

        xRotation = Mathf.Lerp(xRotation, targetXRotation, cameraSmoothness * Time.deltaTime);
        currentYaw = Mathf.LerpAngle(currentYaw, targetYaw, cameraSmoothness * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, currentYaw, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
