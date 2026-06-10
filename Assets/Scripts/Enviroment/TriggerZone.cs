using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onInteract.Invoke();
    }
}
