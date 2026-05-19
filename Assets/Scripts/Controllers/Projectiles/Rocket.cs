
using UnityEngine;

public class Rocket : MonoBehaviour, IUpdatable
{
    public Unit Unit;
    private Ability _referencedAbility;
    private StatsContext _referencedContext;
    private void Awake()
    {
        Unit.OnStartEvent += OnSpawn;
    }
    private void OnSpawn()
    {
        _referencedAbility = Unit.Owner.UnitComponent.Ability;
        _referencedContext = Unit.Owner.StatsContext;
        Registerer.RegisterUpdatable(this);
    }
    public void OnHit(Unit hitUnit)
    {
        _referencedAbility.OnHit(new PositionArgs(transform.position, Quaternion.identity), hitUnit, _referencedContext, Unit.Owner);
        Death();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Environment"))
        {
            OnHit(null);
            return;
        }
        if (other.TryGetComponent(out Unit u))
        {
            if (Unit.UnitComponent.SimComponents.Sensor.IsHitViable(u, Unit) == false) return;

            OnHit(u);
        }
    }
    public void OnUpdate(float deltaTime)
    {
        Unit.UnitComponent.SimComponents.Mover.Move(Unit, transform.forward, deltaTime);
    }
    public void Death()
    {
        Unit.OnStartEvent -= OnSpawn;
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
