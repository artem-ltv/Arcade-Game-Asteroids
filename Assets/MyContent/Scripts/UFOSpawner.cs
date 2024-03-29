using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UFOSpawner : MonoBehaviour
{
    public event UnityAction<bool> IsSpawningUFO;

    [SerializeField] private UFO _ufo;
    [SerializeField] private UFOShooting _shooting;

    private float _topIndent;
    private float _bottomIndent;
    private float _indentProcent;
    private float _spawnPointX;
    private float _variancePointX;

    private float _minDelay;
    private float _maxDelay;

    private void Start()
    {
        _indentProcent = 20f;
        _variancePointX = 1f;
        Vector2 borderScreen = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        float screenHeight = Mathf.Abs(borderScreen.y) * 2;
        _topIndent = screenHeight / 100f * _indentProcent;
        _bottomIndent = -screenHeight / 100f * _indentProcent;
        _spawnPointX = Mathf.Abs(borderScreen.x) + _variancePointX;
        _minDelay = 20f;
        _maxDelay = 40f;

        StartCoroutine(RepeatSpawn());
    }

    public void StartRepeatSpawn()
    {
        _shooting.gameObject.SetActive(false);
        IsSpawningUFO?.Invoke(false);
        StartCoroutine(RepeatSpawn());
    }

    private IEnumerator RepeatSpawn()
    {
        float delay = Random.Range(_minDelay, _maxDelay);
        yield return new WaitForSeconds(delay);
        Spawn();
        _shooting.gameObject.SetActive(true);
        IsSpawningUFO?.Invoke(true);
    }

    private void Spawn()
    {
        float randomSideX = Random.Range(0f, 2f);
        _spawnPointX = randomSideX == 0f ? _spawnPointX : -_spawnPointX;
        float spawnPointY = Random.Range(_bottomIndent, _topIndent);
        Vector2 position = new Vector2(_spawnPointX, spawnPointY);
        Instantiate(_ufo, position, Quaternion.identity);
    }
}
