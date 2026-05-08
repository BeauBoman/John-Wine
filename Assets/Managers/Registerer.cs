using UnityEngine;

public class Registerer : MonoBehaviour
{
    public static void RegisterUpdatable(IUpdatable upd)
    {
        UpdateManager.RegisterUpdate(upd);
    }    
}
