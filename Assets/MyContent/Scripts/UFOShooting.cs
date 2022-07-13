using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UFOShooting : ObjectPool
{
    [SerializeField] private Bullet _bullet;

    private Player _player;
    private UFO _ufo;

    private float _minDelay;
    private float _maxDelay;

    private IEnumerator _shootCoroutine;
    private AudioSource _audioShot;

    private void Start() =>
        _audioShot = GetComponent<AudioSource>();

    private void OnEnable()
    {
        Initialize(_bullet.gameObject);
        _minDelay = 2f;
        _maxDelay = 5f;
        _player = FindObjectOfType<Player>();
        _ufo = FindObjectOfType<UFO>();
        _shootCoroutine = RepeatShot();
        StartCoroutine(_shootCoroutine);
    }
    private void OnDisable() =>
        StopCoroutine(_shootCoroutine);
    

    private IEnumerator RepeatShot()
    {
        while (true)
        {
            float delay = Random.Range(_minDelay, _maxDelay);
            yield return new WaitForSeconds(delay);
            Shot();
            _audioShot.Play();
        }
    }

    private void Shot()
    {
        if(_player != null && _ufo != null)
        {
            if (TryGetObject(out GameObject gameObject))
                if (gameObject.TryGetComponent(out Bullet bullet))
                    SetBullet(bullet);
        }
    }

    private void SetBullet(Bullet bullet)
    {
        Vector3 diffrence = _player.transform.position - _ufo.transform.position;
        bullet.gameObject.SetActive(true);
        bullet.transform.position = _ufo.transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetShootDirection(diffrence.normalized);
        bullet.IsPlayersBullet = false;
    }
}
