using UnityEngine;

public sealed class PlayerCameraController : Controller, IUpdatable
{
    public Unit Unit;
    [SerializeField] private Transform playerCamera;
    private Vector2 PlayerMouseInput;
    private void Start() => Unit.OnSpawn();
    public override void OnStart()
    {
        Registerer.RegisterUpdatable(this);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnUpdate(float deltaTime)
    {
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Unit.UnitSO.SimComponents.Mover.Move(Unit, PlayerMouseInput, deltaTime);
    }
}
