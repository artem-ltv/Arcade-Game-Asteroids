using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.ShotDirection = transform.transform.up;
        }
    }
}
