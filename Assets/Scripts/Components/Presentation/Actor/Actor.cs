using UnityEngine;

public abstract class Actor : ScriptableObject
{
    public abstract void Play(Animator reference);
}