using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))] 
public class Asteroid : MonoBehaviour
{
    public Vector3 MoveDirection;
    public float Size { set => _size = value;}
    
    public float MaxSize { get => _maxSize; }
    
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private Sprite[] _sprites;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private float _size;
    private float _moveSpeed;

    private void Awake() =>
        _size = Random.Range(_minSize, _maxSize);
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.mass = _size * 2f;
        _moveSpeed = _rigidbody.mass / 3f;
        transform.localScale = Vector3.one * _size;
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }

    private void FixedUpdate() =>
        _rigidbody.AddForce(MoveDirection * _moveSpeed);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            if((_size / 2f) >= _minSize)
            {
                CreateHalfAsteroid(_size, -45f);
                CreateHalfAsteroid(_size, 45f);
            }

            FindObjectOfType<Score>().CalculationPoints(_size);
        }

        gameObject.SetActive(false);

        FindObjectOfType<ChekingAsteroids>().CheckLiveAsteroid();

        Destroy(gameObject);
    }

    private void CreateHalfAsteroid(float size, float angle)
    {
        size /= 2f;
        Asteroid halfAsteroid = Instantiate(this, transform.position, transform.rotation);
        halfAsteroid._size = size;
        halfAsteroid.MoveDirection = Quaternion.Euler(0f, 0f, angle) * MoveDirection;
    }
}
