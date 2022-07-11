using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointsCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _healthDisplay;

    private int _health;

    private void Start()
    {
        _health = _player.Health;
        UpdateDisplayHealthPoints();
    }

    private void OnEnable() =>
        _player.ChangingCountHealth += ChangeCountHealth;

    private void OnDisable() =>
        _player.ChangingCountHealth -= ChangeCountHealth;

    private void ChangeCountHealth(int damage)
    {
        _health -= damage;
        UpdateDisplayHealthPoints();
    }

    private void UpdateDisplayHealthPoints() =>
        _healthDisplay.text = _health.ToString();
}
