using UnityEngine;

public abstract class Behaviour : ScriptableObject
{
    public float Duration;
    public float Period;
    internal abstract void OnStart();
    internal abstract void OnUpdate(float dt);
    internal abstract void OnEnd();
}