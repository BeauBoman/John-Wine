using UnityEngine;

public class DummyController : MonoBehaviour
{
    public Unit Unit;
    private void Start()
    {
        Unit.OnStart();
        Unit.OnHealthIsZero += Death;
    }
    public void Death()
    {
        Unit.OnHealthIsZero -= Death;
        Destroy(gameObject);
    }
}
