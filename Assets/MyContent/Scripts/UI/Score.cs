using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreDisplay;

    private int _score;

    public void CalculationPoints(float sizeAsteroid)
    {
        if (sizeAsteroid <= 0.6f)
            _score += 100;

        else if (sizeAsteroid <= 0.9f)
            _score += 50;

        else
            _score += 20;

        UpdateDisplayScore();
    }

    public void CalculationPoints(UFO ufo)
    {
        _score += 200;
        UpdateDisplayScore();
    }

    private void UpdateDisplayScore() =>
        _scoreDisplay.text = _score.ToString();
}
