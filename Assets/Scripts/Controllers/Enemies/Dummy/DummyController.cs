using UnityEngine;

public sealed class DummyController : Controller
{
    public Unit Unit;
    private void Start() => Unit.OnSpawn();
    public override void OnStart()
    {
        Unit.OnHealthIsZero += Death;
        Debug.Log(Unit.State.HealthState.CurrentHealth);
    }
    public void Death()
    {
        Unit.OnHealthIsZero -= Death;
        Destroy(gameObject);
    }
}
