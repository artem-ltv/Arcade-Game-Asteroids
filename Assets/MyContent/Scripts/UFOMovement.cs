using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private bool _isMoveRight;
    private void Start() =>
        _isMoveRight = transform.position.x < 0 ? true : false;
    

    private void Update()
    {
        if (_isMoveRight)
            SetMovement(Vector2.right, _moveSpeed);

        else
            SetMovement(-Vector2.right, _moveSpeed);
    }

    private void SetMovement(Vector2 direction, float speed) =>
        transform.Translate(direction * speed * Time.deltaTime);
}
