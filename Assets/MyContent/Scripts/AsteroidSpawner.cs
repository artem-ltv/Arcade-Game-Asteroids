using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid _asteriod;
    [SerializeField] private float _trajectoryVariance;
    [SerializeField] private ChekingAsteroids _chekingAsteroids;

    private float _spawnRadius;
    private int _spawnAmount;
    private float _spawnDelay;
    private bool isInitial;

    private IEnumerator Start()
    {
        _spawnRadius = 9f;
        _spawnDelay = 2f;
        yield return new WaitForSeconds(_spawnDelay);
        SpawnInitialAsteroids();
    }

    private void OnEnable() =>
        _chekingAsteroids.AbsenceOfAsteroids += StartRepeatSpawn;

    private void OnDisable() =>
        _chekingAsteroids.AbsenceOfAsteroids -= StartRepeatSpawn;

    private void StartRepeatSpawn() =>
        StartCoroutine(RepeatSpawn());

    private void SpawnInitialAsteroids()
    {
        isInitial = true;
        _spawnAmount = 2;
        for(int i = 0; i<_spawnAmount; i++)
            Spawn(isInitial);
    }

    private IEnumerator RepeatSpawn()
    {
        yield return new WaitForSeconds(_spawnDelay);

        _spawnAmount++;
        for (int i = 0; i < _spawnAmount; i++)
            Spawn();
    }


    private void Spawn(bool isInitial = false)
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnRadius;
        float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);

        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        Asteroid newAsteroid = Instantiate(_asteriod, spawnDirection, rotation);
        newAsteroid.MoveDirection = rotation * -spawnDirection;

        if (isInitial)
            newAsteroid.Size = newAsteroid.MaxSize;
    }
}
