using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyController : Controller, IUpdatable
{
    public Transform FirePoint;
    private EnemyPathfinding _pf;
    private void Start()
    {
        _unit.OnSpawn();
        _pf = GetComponent<EnemyPathfinding>();
    }
    public override void OnStart()
    {
        _unit.OnHealthIsZero += Death;
        Registerer.RegisterUpdatable(this);
        _unit.ChangeAbility(0);
    }
    public void OnUpdate(float dt)
    {
        _unit.OnUpdate(dt);
        _unit.State.CurrentAbility.ReloadProgress(dt);
        HandleWeapon();
    }
    private void HandleWeapon()
    {
        if (_pf._token == true)
        {
            if (_unit.State.CurrentAbility.CanShoot == false) return;

            _unit.State.CurrentAbility.Fire(new PositionArgs(_unit.Turret.position, _unit.Turret.rotation, _unit.Turret.forward), new PositionArgs(FirePoint.position, FirePoint.rotation, FirePoint.forward), _unit);
            _unit.State.CurrentAbility.ResetReloadProgress();
        }
    }
    public void Death()
    {
        _unit.OnHealthIsZero -= Death;
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
