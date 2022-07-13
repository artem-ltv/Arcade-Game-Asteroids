using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Menu _menu;

    private int _typeControl;
    private Rigidbody2D _rigidbody;

    private Vector2 _currentDirection;
    private Vector2 _borderScreen;
    private float borderScreenX;
    private float borderScreenY;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentDirection = new Vector3(0f, 1f, 0f);
        _borderScreen = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        borderScreenX = Mathf.Abs(_borderScreen.x);
        borderScreenY = Mathf.Abs(_borderScreen.y);
    }

    private void OnEnable() =>
        _menu.SwitchingControl += ChangeTypeControl;

    private void OnDisable() =>
        _menu.SwitchingControl -= ChangeTypeControl;

    private void Update()
    {
        if (transform.position.x > borderScreenX)
            MoveToOppositeSide(-borderScreenX, transform.position.y, 20f);

        if (transform.position.x < -borderScreenX)
            MoveToOppositeSide(borderScreenX, transform.position.y, 20f);

        if (transform.position.y > borderScreenY)
            MoveToOppositeSide(transform.position.x, -borderScreenY, 20f);

        if (transform.position.y < -borderScreenY)
            MoveToOppositeSide(transform.position.x, borderScreenY, 20f);
    }
    
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _rigidbody.AddForce(transform.up * _moveSpeed);


        if(_typeControl == 1)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                _rigidbody.AddTorque(-_turnSpeed);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                _rigidbody.AddTorque(_turnSpeed);
        }

        if (_typeControl == 2)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 position = transform.position;
            Vector2 direction = mousePosition - position;

            _currentDirection = Vector2.Lerp(_currentDirection, direction, _turnSpeed * 2 * Time.deltaTime);
            transform.up = _currentDirection;
        }
    }
    private void MoveToOppositeSide(float borderScreenPointX, float borderScreenPointY, float maxDistanceDelta) =>
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(borderScreenPointX, borderScreenPointY), maxDistanceDelta);

    private void ChangeTypeControl(int type) =>
        _typeControl = type;
}
