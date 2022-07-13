using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ExplosionAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundsExplosion;

    private AudioSource _audioExplosion;

    private void Start() =>
        _audioExplosion = GetComponent<AudioSource>();
    
    public void PlayAudioExplosion(float size)
    {
        if (size <= 0.6f)
            _audioExplosion.PlayOneShot(_soundsExplosion[0]);

        else if (size <= 0.9f)
            _audioExplosion.PlayOneShot(_soundsExplosion[1]);

        else
            _audioExplosion.PlayOneShot(_soundsExplosion[2]);
    }

    public void PlayAudioExplosion() =>
        _audioExplosion.PlayOneShot(_soundsExplosion[2]);
    
}
