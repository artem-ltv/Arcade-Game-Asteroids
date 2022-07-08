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

        else if (sizeAsteroid <= 0.9)
            _score += 50;

        else
            _score += 20;

        _scoreDisplay.text = _score.ToString();
    }
}
