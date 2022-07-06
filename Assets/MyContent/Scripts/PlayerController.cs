using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    private Vector3 _vectorRotation;
    private Rigidbody2D _rigidbody;

    private void Start() =>
        _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _rigidbody.AddForce(transform.up * _moveSpeed);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            _rigidbody.AddTorque(-_turnSpeed);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            _rigidbody.AddTorque(_turnSpeed);
    }
}
