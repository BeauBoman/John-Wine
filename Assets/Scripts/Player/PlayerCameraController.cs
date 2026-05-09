using UnityEngine;

public class PlayerCameraController : MonoBehaviour, IUpdatable
{
    [SerializeField] Unit Unit;
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
        Unit.Components.Mover.Move(Unit, PlayerMouseInput, deltaTime);
    }
}
