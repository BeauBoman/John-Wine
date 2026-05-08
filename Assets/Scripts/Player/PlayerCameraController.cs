using UnityEngine;

public class PlayerCameraController : Unit, IUpdatable
{
    public Transform playerCamera;
    private Vector2 PlayerMouseInput;
    private void Start()
    {
        Registerer.RegisterUpdatable(this);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnUpdate(float deltaTime)
    {
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Components.Mover.Move(this, PlayerMouseInput, deltaTime);
    }
}
