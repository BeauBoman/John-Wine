using UnityEngine;

public sealed class RocketController : Controller, IUpdatable, IAbilityConfigCarrier
{
    public Unit Unit;

    public AbilitySO abilitySO { get; set; }
    public sealed override void OnStart()
    {
        Unit.Stats.SetComponentsStats(abilitySO.ImpactComponents);

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
        abilitySO.OnHit(Unit.Stats, new PositionArgs(transform.position, transform.rotation, transform.forward), Unit.Owner, hitUnit);
        Death();
    }
    public void Death()
    {
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
