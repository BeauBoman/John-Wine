using UnityEngine;

public class Avatar : MonoBehaviour
{
    public PresentationComponents Components;
}
public struct PresentationComponents
{
    public DefaultActor Anim;
    public ShuffleSpeaker Audio;
    public DefaultEmitter Particles;
}