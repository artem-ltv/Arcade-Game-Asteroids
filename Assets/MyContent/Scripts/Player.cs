using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) =>
        Die();

    private void Die() =>
        Destroy(gameObject);
}
