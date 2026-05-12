using UnityEngine;
[CreateAssetMenu(fileName = "Default Emitter", menuName = "Components/Presentation/Emitter/Default Emitter")]
public class DefaultEmitter : Emitter
{
    public override ParticleSystem Play(ParticleSystem sys)
    {
        sys.Play();
        return sys;
    }
}
