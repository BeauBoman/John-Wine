using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyController : Controller, IUpdatable
{
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
            Debug.Log("token aquired");
            if (_unit.State.CurrentAbility.CanShoot == false) return;

            Debug.Log("fired enemy");
            _unit.State.CurrentAbility.Fire(new PositionArgs(transform.position, transform.rotation, transform.forward), _unit);
            _unit.State.CurrentAbility.ResetReloadProgress();
        }
        else
            Debug.Log("Token denied");
    }
    public void Death()
    {
        _unit.OnHealthIsZero -= Death;
        Registerer.UnregisterUpdatable(this);

        gameObject.GetComponent<EnemyPathfinding>().Death();
        Destroy(gameObject);
    }
}
