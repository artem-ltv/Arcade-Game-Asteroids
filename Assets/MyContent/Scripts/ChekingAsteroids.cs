using UnityEngine;
using UnityEngine.Events;

public class ChekingAsteroids : MonoBehaviour
{
    public event UnityAction AbsenceOfAsteroids;

    [SerializeField] private Asteroid[] _asteroidsOnScene;

    public void CheckLiveAsteroid()
    {
        _asteroidsOnScene = FindObjectsOfType<Asteroid>();

        if (_asteroidsOnScene.Length == 0)
            AbsenceOfAsteroids?.Invoke();
    } 
}
