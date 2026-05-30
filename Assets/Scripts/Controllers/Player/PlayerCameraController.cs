using UnityEngine;

public sealed class PlayerCameraController : Controller, IUpdatable
{
    public Unit Unit;
    [SerializeField] private Transform playerCamera;
    private Vector2 PlayerMouseInput;
    private ItemSys itemSys;
    private void Start() => Unit.OnSpawn();
    public override void OnStart()
    {
        itemSys = GetComponent<ItemSys>();
        Registerer.RegisterUpdatable(this);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnUpdate(float deltaTime)
    {
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Unit.UnitSO.SimComponents.Mover.Move(Unit, PlayerMouseInput, deltaTime);

        HandleInteraction();
    }
    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Unit.UnitSO.SimComponents.Raycaster.Raycast(transform.position, transform.forward, 3f).collider != null)
            {
                Collider target = Unit.UnitSO.SimComponents.Raycaster.Raycast(transform.position, transform.forward, 3f).collider;
                if (target.TryGetComponent(out Unit u))
                {
                    if (u.UnitSO.Tags.HasFlag(Tags.Key))
                    {
                        itemSys.AddItem(target.gameObject);
                        target.gameObject.SetActive(false);
                    }                    
                } else
                {
                    InteractiveBaseEnviroment enviromentTarget = target.GetComponent<InteractiveBaseEnviroment>();
                    if (target != null)
                    {
                        if (itemSys.GetKey(enviromentTarget) == true)
                        {
                            enviromentTarget.Interact();
                        }
                    }
                }
                
            }          
        }
    }
}
