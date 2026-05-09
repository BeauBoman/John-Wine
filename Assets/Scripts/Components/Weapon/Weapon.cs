using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [Header("Launch")]
    public Effect LaunchEffect;
    public Behaviour LaunchBehaviour;
    public Detector LaunchDetector;
    public Actor LaunchActor;
    [Header("Impact")]
    public Effect ImpactEffect;
    public Behaviour ImpactBehaviour;
    public Detector ImpactDetector;
    public Actor ImpactActor;
    public abstract void Fire();
}
