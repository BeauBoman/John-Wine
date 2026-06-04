using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public List<IUpdatable> Updates = new List<IUpdatable>();
    public HashSet<IUpdatable> RegisteredUpdates = new HashSet<IUpdatable>();

    private List<IUpdatable> _toRegister = new List<IUpdatable>();
    private List<IUpdatable> _toUnregister = new List<IUpdatable>();
    private bool _isUpdating;
    private float _deltaTime;

    public static UpdateManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Update()
    {
        ProcessQueue();
        _isUpdating = true;

        _deltaTime = Time.deltaTime;
        for (int i = Updates.Count - 1; i >= 0; i--)
        {
            if (Updates[i] != null)
                Updates[i].OnUpdate(_deltaTime);
        }

        _isUpdating = false;
        ProcessQueue();
    }
    private void ProcessQueue()
    {
        for (int i = _toRegister.Count - 1; i >= 0; i--)
        {
            RegisterUpdate(_toRegister[i]);
        }
        for (int i = _toUnregister.Count - 1; i >= 0; i--)
        {
            UnregisterUpdate(_toUnregister[i]);
        }
    }
    public void RegisterUpdate(IUpdatable upd)
    {
        if (Instance == null) { Debug.LogError("Update manager instance is null. Register method was called too early." + upd.GetType()); return; }
        if (_isUpdating == false)
        {
            if (RegisteredUpdates.Contains(upd)) return;

            Updates.Add(upd);
            RegisteredUpdates.Add(upd);
        }
        else
        {
            _toRegister.Add(upd);
        }
    }
    public void UnregisterUpdate(IUpdatable upd)
    {
        if (_isUpdating == false)
        {
            if (RegisteredUpdates.Contains(upd) == false) return;

            Updates.Remove(upd);
            RegisteredUpdates.Remove(upd);
        }
        else
        {
            _toUnregister.Add(upd);
        }
    }
}
public interface IUpdatable
{
    void OnUpdate(float deltaTime);
}
