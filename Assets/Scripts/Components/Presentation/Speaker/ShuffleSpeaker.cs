using UnityEngine;

[CreateAssetMenu(fileName = "Shuffle Speaker", menuName = "Components/Presentation/Speaker/Shuffle Speaker")]
public class ShuffleSpeaker : ScriptableObject
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] int _lastIndex = -1;
    [SerializeField] float _maxVolume;
    [SerializeField] float _minVolume;
    [SerializeField] float _maxPitch;
    [SerializeField] float _minPitch;

    public void Play(AudioSource reference)
    {
        int idx = Random.Range(0, _clips.Length);

        if (idx == _lastIndex)
        {
            idx = (idx + 1) % _clips.Length;
        }
        _lastIndex = idx;

        reference.volume = Random.Range(_minVolume, _maxVolume);
        reference.pitch = Random.Range(_minPitch, _maxPitch);

        reference.clip = _clips[idx];

        reference.PlayOneShot(_clips[idx]);
    }
}
