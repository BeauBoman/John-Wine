using UnityEngine;

public class Registerer : MonoBehaviour
{
    public static void RegisterUpdatable(IUpdatable upd)
    {
        UpdateManager.RegisterUpdate(upd);
    }    
    public static void UnregisterUpdatable(IUpdatable upd)
    {
        UpdateManager.UnregisterUpdate(upd);
    }
}
