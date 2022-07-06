using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid _asteriod;
    [SerializeField] private float _trajectoryVariance;

    private float _spawnRadius;
    private int _spawnAmount;

    private void Start()
    {
        _spawnRadius = 10;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        _spawnAmount++;
        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector2 spawnDirection = Random.insideUnitCircle.normalized * _spawnRadius;
            float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);

            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid newAsteroid = Instantiate(_asteriod, spawnDirection, rotation);
            newAsteroid.MoveDirection = rotation * -spawnDirection;
        }

        StartCoroutine(Spawn());
    }
}
