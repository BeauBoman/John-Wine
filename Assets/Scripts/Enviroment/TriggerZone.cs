using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    private bool isActived = false;
    private void OnTriggerEnter(Collider collision)
    {
        if (isActived == false)
        {
            if (collision.CompareTag("Player"))
            {
                onInteract.Invoke();
                isActived = true;
            }
        }
    }
}
