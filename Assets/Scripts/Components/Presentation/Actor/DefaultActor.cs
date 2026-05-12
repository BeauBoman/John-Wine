using UnityEngine;

[CreateAssetMenu(fileName = "Default Actor", menuName = "Components/Presentation/Actor/Default Actor")]
public class DefaultActor : Actor
{
    [SerializeField] string _animationName;
    public override void Play(Animator reference)
    {
        reference.Play(_animationName);
    }
}
