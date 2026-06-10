using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveBaseEnviroment : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    public GameObject key;
    public Transform keyPos;

    [Header("Rotation")]
    [SerializeField] private Vector3[] targetRotation;
    [SerializeField] private float rotationTime;
    private int rotationIndex = 0;
    private bool isRotating = false;

    [Header("Change Position")]
    [SerializeField] private Vector3[] targetPosition;
    [SerializeField] private float positionTime;
    private int positionIndex = 0;
    private bool isChangingPosition = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem effect;
    public void Interact()
    {
        onInteract.Invoke();
    }
    public void Rotate()
    {
        if (isRotating == true)
            return;
        isRotating = true;
        StartCoroutine(RotateOverTime(transform.localRotation * Quaternion.Euler(targetRotation[rotationIndex]), rotationTime, GetComponent<Rigidbody>()));

        rotationIndex++;
        if (rotationIndex >= targetRotation.Length)
        {
            rotationIndex = 0;
        }
    }
    private IEnumerator RotateOverTime(Quaternion targetRotation, float time, Rigidbody rb)
    {
        Quaternion startRotation = transform.localRotation;
        float elapsedTime = 0f;

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        while (elapsedTime < time)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = targetRotation;

        if (rb != null)
        {
            rb.isKinematic = false;
        }
        isRotating = false;
    }
    public void ChangePosition()
    {
        if (isChangingPosition == true)
            return;
        isChangingPosition = true;

        StartCoroutine(ChangePositionOverTime(targetPosition[positionIndex], positionTime, GetComponent<Rigidbody>()));

        positionIndex++;
        if (positionIndex >= targetPosition.Length)
        {
            positionIndex = 0;
        }
    }
    private IEnumerator ChangePositionOverTime(Vector3 targetPosition, float time, Rigidbody rb)
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        while (elapsedTime < time)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPosition;
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        isChangingPosition = false;
    }
    public void KeyPositionChange()
    {
        if (key != null)
        {
            if (keyPos != null)
            {
                key.SetActive(true);
                key.transform.position = keyPos.position;
                key.transform.rotation = keyPos.rotation;
            } else
            {
                Destroy(key);
            }
        }
    }
    public bool CheckKey(GameObject playerKey)
    {
        if (key == null)
            return true;
        else if (playerKey == key)
            return true;
        else
            return false;
    }
    public void TriggerEffect()
    {
        if (effect == null) return;
        effect.Play();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
