using UnityEngine;

public class DoubleScytheController : Controller, IUpdatable, IAbilityConfigCarrier
{
    public AbilitySO abilitySO { get; set; }

    public override void OnStart()
    {
        _unit.Stats.SetComponentsStats(abilitySO.ImpactComponents);

        Registerer.RegisterUpdatable(this);
    }

    public void OnUpdate(float deltaTime)
    {
        if (this == null) return;
        _unit.UnitSO.SimComponents.Movers.Mover.Move(_unit, transform.forward, deltaTime);
    }
}
