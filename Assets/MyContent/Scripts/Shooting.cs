using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private float _shotDelay;
    private bool _isRecharged;

    private void Start()
    {
        _shotDelay = 0.334f;
        _isRecharged = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
           StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        if (_isRecharged)
        {
            _isRecharged = false;
            Bullet bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.ShotDirection = transform.transform.up;
            bullet.IsPlayersBullet = true;
            yield return new WaitForSeconds(_shotDelay);
            _isRecharged = true;
        }
    }
}
