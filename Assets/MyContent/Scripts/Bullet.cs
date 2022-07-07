using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 ShotDirection;

    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private void Update() =>
        transform.Translate(ShotDirection * _speed * Time.deltaTime);

    private void OnCollisionEnter2D(Collision2D collision) =>
        Destroy(gameObject);
}
