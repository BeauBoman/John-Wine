using UnityEngine;

public class PlayerCameraController : MonoBehaviour, IUpdatable
{
    public Unit Unit;
    [SerializeField] private Transform playerCamera;
    private Vector2 PlayerMouseInput;
    private void Start()
    {
        Registerer.RegisterUpdatable(this);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnUpdate(float deltaTime)
    {
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Unit.UnitStats.SimComponents.Mover.Move(Unit, PlayerMouseInput, deltaTime);
    }
}
