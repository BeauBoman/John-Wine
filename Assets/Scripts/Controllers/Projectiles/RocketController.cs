using UnityEngine;

public sealed class RocketController : Controller, IUpdatable
{
    public Unit Unit;

    private Ability _ability;
    public sealed override void OnStart()
    {
        _ability = Unit.Owner.State.CurrentAbility;
        Unit.Stats.SetComponentsStats(_ability.config.ImpactComponents);
        Registerer.RegisterUpdatable(this);
    }
    public void OnUpdate(float deltaTime)
    {
        Unit.UnitSO.SimComponents.Mover.Move(Unit, transform.forward, deltaTime);
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
            if (Unit.UnitSO.SimComponents.Sensor.IsDetectionViable(Unit.Stats, u, Unit) == false) return;

            OnHit(u);
        }
    }
    public void OnHit(Unit hitUnit)
    {
        _ability.OnHit(new PositionArgs(transform.position, Quaternion.identity), Unit, hitUnit);
        Death();
    }
    public void Death()
    {
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
