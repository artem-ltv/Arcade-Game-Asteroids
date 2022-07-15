using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction<int> ChangingCountHealth;
    public event UnityAction Dying;
    public int Health { get => _health; }

    [SerializeField] private int _health;
    [SerializeField] private float _timeInvulnerability;

    private bool _isInvulnerability;
    private ExplosionAudioManager _explosionSound;
    private IEnumerator Start()
    {
        _explosionSound = FindObjectOfType<ExplosionAudioManager>();
        _isInvulnerability = true;
        yield return new WaitForSeconds(_timeInvulnerability);
        _isInvulnerability = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isInvulnerability)
        {
            if(collision.gameObject.TryGetComponent(out Asteroid asteroid))
                AddDamage(_health);

            if (collision.gameObject.TryGetComponent(out Bullet bullet))
                AddDamage(1);
        }
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
        Dying?.Invoke();
        Destroy(gameObject);
    }
}
