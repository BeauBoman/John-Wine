using UnityEngine;
using UnityEngine.Events;

public class Multikeychecker : MonoBehaviour
{
    public static Multikeychecker instance;

    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private int keyToTrigger;
    private int keysAquired;
    private void Start()
    {
        instance = this;
    }
    public void AddKey()
    {
        keysAquired++;
        CheckKeys();
    }
    private void CheckKeys()
    {
        if (keysAquired >= keyToTrigger)
        {
            onInteract.Invoke();
        }
    }
}
