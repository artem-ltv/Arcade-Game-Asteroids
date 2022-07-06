using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))] 
public class Asteroid : MonoBehaviour
{
    public Vector2 MoveDirection;

    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private Sprite[] _sprites;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private float _size;
    private float _moveSpeed;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _size = Random.Range(_minSize, _maxSize);
        transform.localScale = Vector3.one * _size;
        _rigidbody.mass = _size * 2f;
        _moveSpeed =  _rigidbody.mass / 3f;
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
        //_moveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(MoveDirection * _moveSpeed);
    }
}
