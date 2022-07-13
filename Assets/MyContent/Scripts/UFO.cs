using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UFO : MonoBehaviour
{
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
        FindObjectOfType<UFOSpawner>().StartRepeatSpawn();
        Destroy(gameObject);
    }

    private void AddScore() =>
        FindObjectOfType<Score>().CalculationPoints(this);

}
