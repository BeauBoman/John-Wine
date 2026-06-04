using UnityEngine;

public class Registerer : MonoBehaviour
{
    public static void RegisterUpdatable(IUpdatable upd)
    {
        UpdateManager.Instance.RegisterUpdate(upd);
    }    
    public static void UnregisterUpdatable(IUpdatable upd)
    {
        UpdateManager.Instance.UnregisterUpdate(upd);
    }
}
