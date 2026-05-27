using UnityEngine;

public sealed class Rocket : Controller, IUpdatable
{
    public Unit Unit;

    private AbilitySO _abilityConfig;
    private ComponentRuntimeStats _impactStats;
    public sealed override void OnStart()
    {
        _impactStats = Unit.Owner.State.CurrentAbility.GetImpactComponentsForProjectile();
        _abilityConfig = Unit.Owner.State.CurrentAbility.config;
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
        _abilityConfig.OnHit(_impactStats, new PositionArgs(transform.position, Quaternion.identity), Unit, hitUnit);
        Death();
    }
    public void Death()
    {
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
