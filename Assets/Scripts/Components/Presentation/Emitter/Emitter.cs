using UnityEngine;

public abstract class Emitter : ScriptableObject
{
    [SerializeField] protected ParticleSystem _particleSystem;
    public abstract ParticleSystem Play(ParticleSystem ps);
}
