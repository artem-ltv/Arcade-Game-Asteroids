using System.Collections;
using UnityEngine;

public class AsteroidSpawner : ObjectPool
{
    [SerializeField] private Asteroid _asteriod;
    [SerializeField] private float _trajectoryVariance;
    [SerializeField] private ChekingAsteroids _chekingAsteroids;

    private float _spawnRadius;
    private int _spawnAmount;
    private float _spawnDelay;
    private bool _isInitial;

    private void Start()
    {
        Initialize(_asteriod.gameObject);
        _spawnRadius = 7.5f;
        _spawnDelay = 2f;
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
        _isInitial = true;
        _spawnAmount = 2;
        for(int i = 0; i<_spawnAmount; i++)
            Spawn(_isInitial);
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
        if(TryGetObject(out GameObject gameObject))
        {
            if (gameObject.TryGetComponent(out Asteroid asteroid));
            SetAsteroid(asteroid, isInitial);
        }
    }

    public void SpawnHalf(float size, float angle, Vector2 position, Quaternion rotation, Vector3 moveDirection)
    {
        if (TryGetObject(out GameObject gameObject))
        {
            if (gameObject.TryGetComponent(out Asteroid asteroid)) 
                SetAsteroid(asteroid, size, angle, position, rotation, moveDirection);
        }
    }

    public void SetAsteroid(Asteroid halfAsteroid, float size, float angle, Vector2 position, Quaternion rotation, Vector3 moveDirection)
    {
        halfAsteroid.gameObject.SetActive(true);
        halfAsteroid.transform.position = position;
        halfAsteroid.transform.rotation = rotation;
        halfAsteroid.SetSize(size);
        halfAsteroid.MoveDirection = Quaternion.Euler(0f, 0f, angle) * moveDirection;
    }
    private void SetAsteroid(Asteroid asteroid, bool isInitial)
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnRadius;
        float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        asteroid.gameObject.SetActive(true);
        asteroid.transform.position = spawnDirection;
        asteroid.transform.rotation = rotation;
        asteroid.MoveDirection = rotation * -spawnDirection;

        if (isInitial)
            asteroid.SetSize(asteroid.MaxSize);
    }

}
