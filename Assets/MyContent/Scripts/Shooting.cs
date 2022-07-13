using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooting : ObjectPool
{
    [SerializeField] private Player _player;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Menu _menu;

    private float _shotDelay;
    private bool _isRecharged;
    private int _typeControl;
    private AudioSource _audioShot;

    private void Start()
    {
        Initialize(_bullet.gameObject);
        _shotDelay = 0.334f;
        _isRecharged = true;
        _audioShot = GetComponent<AudioSource>();
    }

    private void OnEnable() =>
        _menu.SwitchingControl += ChangeTypeControl;

    private void OnDisable() =>
        _menu.SwitchingControl -= ChangeTypeControl;


    private void Update()
    {

        if (_typeControl == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(Shot());
        }

        if (_typeControl == 2)
        {
            if(Input.GetMouseButton(0))
                StartCoroutine(Shot());
        }
    }

    private IEnumerator Shot()
    {
        if (_isRecharged)
        {
            _isRecharged = false;

            if (TryGetObject(out GameObject gameObject))
                if (gameObject.TryGetComponent(out Bullet bullet))
                    SetBullet(bullet);

            _audioShot.Play();
            yield return new WaitForSeconds(_shotDelay);
            _isRecharged = true;
        }
    }

    private void SetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = _player.transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetShootDirection(_player.transform.up);
        bullet.IsPlayersBullet = true;
    }

    private void ChangeTypeControl(int type) =>
        _typeControl = type;
}
