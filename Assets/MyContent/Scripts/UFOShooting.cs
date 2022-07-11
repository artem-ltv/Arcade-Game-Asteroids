using System.Collections;
using UnityEngine;

public class UFOShooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private Player _player;

    private float _minDelay;
    private float _maxDelay;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _minDelay = 2f;
        _maxDelay = 5f;
        StartCoroutine(RepeatShot());
    }
    
    private IEnumerator RepeatShot()
    {
        while (true)
        {
            float delay = Random.Range(_minDelay, _maxDelay);
            yield return new WaitForSeconds(delay);
            Shot();
        }
    }

    private void Shot()
    {
        if(_player != null)
        {
            Vector3 diffrence = _player.transform.position - transform.position;
            Bullet newBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            newBullet.ShotDirection = diffrence.normalized;
            newBullet.IsPlayersBullet = false;
        }
    }
}
