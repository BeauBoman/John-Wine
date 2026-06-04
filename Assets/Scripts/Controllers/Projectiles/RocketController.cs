using UnityEngine;

public sealed class RocketController : Controller, IUpdatable, IAbilityConfigCarrier
{
    public AbilitySO abilitySO { get; set; }
    public sealed override void OnStart()
    {
        _unit.Stats.SetComponentsStats(abilitySO.ImpactComponents);

        Registerer.RegisterUpdatable(this);
    }
    public void OnUpdate(float deltaTime)
    {
        if (this == null) return;
        _unit.UnitSO.SimComponents.Movers.Mover.Move(_unit, transform.forward, deltaTime);
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
            if (_unit.UnitSO.SimComponents.Sensor.IsDetectionViable(_unit.Stats, u, _unit) == false) return;

            OnHit(u);
        }
    }
    public void OnHit(Unit hitUnit)
    {
        abilitySO.OnHit(_unit.Stats, new PositionArgs(transform.position, transform.rotation, transform.forward), _unit.Owner, hitUnit);
        _unit.Die();
    }
    public override void OnDeath()
    {
        Registerer.UnregisterUpdatable(this);
    }
}
