using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public List<IUpdatable> Updates = new List<IUpdatable>();
    public HashSet<IUpdatable> RegisteredUpdates = new HashSet<IUpdatable>();

    float deltaTime;

    public static UpdateManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Update()
    {
        deltaTime = Time.deltaTime;
        for (int i = Updates.Count - 1; i >= 0; i--)
        {
            Updates[i].OnUpdate(deltaTime);
        }
    }
    
    public static void RegisterUpdate(IUpdatable upd)
    {
        if (Instance == null) Debug.Log("Instance is null");
        if (Instance.RegisteredUpdates.Contains(upd)) return;

        Instance.Updates.Add(upd);
        Instance.RegisteredUpdates.Add(upd);
    }
    public static void UnregisterUpdate(IUpdatable upd)
    {
        if(Instance.RegisteredUpdates.Contains(upd) == false) return;

        Instance.Updates.Remove(upd);
        Instance.RegisteredUpdates.Remove(upd);
    }
}
public interface IUpdatable
{
    void OnUpdate(float deltaTime);
}
