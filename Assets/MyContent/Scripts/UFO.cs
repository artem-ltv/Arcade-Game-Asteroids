using UnityEngine;

public class UFO : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet bullet))
            AddScore();

        Die();
    }

    private void Die()
    {
        FindObjectOfType<UFOSpawner>().StartRepeatSpawn();
        Destroy(gameObject);
    }

    private void AddScore() =>
        FindObjectOfType<Score>().CalculationPoints(this);
    
}
