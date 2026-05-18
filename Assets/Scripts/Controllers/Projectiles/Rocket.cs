using UnityEngine;

public class Rocket : MonoBehaviour, IUpdatable
{
    public Unit Unit;
    private void Awake()
    {
  
    }
    private void Start()
    {
        Unit.OnStart();
        Registerer.RegisterUpdatable(this);
    }
    public void OnHit(Unit hitUnit)
    {
        if(hitUnit != null)
        {
            Debug.Log(hitUnit);
            hitUnit.TakeDamage(50);
        }
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
    
        Registerer.UnregisterUpdatable(this);

        Destroy(gameObject);
    }
}
