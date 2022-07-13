using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction<int> ChangingCountHealth;
    public int Health { get => _health; }

    [SerializeField] private int _health;

    private ExplosionAudioManager _explosionSound;    

    private void Start() =>
        _explosionSound = FindObjectOfType<ExplosionAudioManager>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Asteroid asteroid))
            AddDamage(_health);

        if (collision.gameObject.TryGetComponent(out Bullet bullet))
            AddDamage(1);
    }

    public void AddDamage(int damage)
    {
        _health -= damage;
        ChangingCountHealth?.Invoke(damage);

        if (_health <= 0)
            Die();
    }

    public void Die()
    {
        _explosionSound.PlayAudioExplosion();
        Destroy(gameObject);
    }
}
