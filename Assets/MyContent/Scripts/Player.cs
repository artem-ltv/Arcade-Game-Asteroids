using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Asteroid asteroid))
            Die();

        if (collision.gameObject.TryGetComponent(out Bullet bullet))
            Die();
    }

    public void Die() =>
        Destroy(gameObject);
}
