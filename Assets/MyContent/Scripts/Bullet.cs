using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public Vector2 ShotDirection;
    public bool IsPlayersBullet;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Color _playersBullet;
    [SerializeField] private Color _ufoBullet;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void Start() 
    {
        Physics2D.IgnoreLayerCollision(7, 8, IsPlayersBullet);
        Physics2D.IgnoreLayerCollision(8, 9, !IsPlayersBullet);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = IsPlayersBullet ? _playersBullet : _ufoBullet;
    }
    
    private void Update() =>
        transform.Translate(ShotDirection * _moveSpeed * Time.deltaTime);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible() =>
        Destroy(gameObject);
}
