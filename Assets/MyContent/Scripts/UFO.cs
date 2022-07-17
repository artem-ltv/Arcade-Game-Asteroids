using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UFO : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;

    private ExplosionAudioManager _explosionAudio;

    private void Start() =>
        _explosionAudio = FindObjectOfType<ExplosionAudioManager>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet bullet))
            AddScore();

        Die();
    }

    private void Die()
    {
        _explosionAudio.PlayAudioExplosion();
        ParticleSystem deathEffect = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        deathEffect.Play();
        FindObjectOfType<UFOSpawner>().StartRepeatSpawn();
        Destroy(gameObject);
    }

    private void AddScore() =>
        FindObjectOfType<Score>().CalculationPoints(this);

}
