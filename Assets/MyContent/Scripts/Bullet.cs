using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public bool IsPlayersBullet;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Color _playersBullet;
    [SerializeField] private Color _ufoBullet;

    private Vector2 _shotDirection;
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

    private void OnEnable()
    {
        Physics2D.IgnoreLayerCollision(7, 8, IsPlayersBullet);
        Physics2D.IgnoreLayerCollision(8, 9, !IsPlayersBullet);
    }

    private void OnDisable()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void Update() =>
        transform.Translate(_shotDirection * _moveSpeed * Time.deltaTime);

    private void OnCollisionEnter2D(Collision2D collision) =>
        gameObject.SetActive(false);
    

    private void OnBecameInvisible() =>
        gameObject.SetActive(false);

    public void SetShootDirection(Vector2 direction) =>
        _shotDirection = direction;
    
}
