using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosingPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _losingPanel;
    [SerializeField] private Button _buttonRestart;

    private void Start() =>
        _buttonRestart.onClick.AddListener(ReloadScene);   
    

    private void OnEnable() =>
        _player.Dying += ShowLosingPanel;

    private void OnDisable() =>
        _player.Dying -= ShowLosingPanel;

    private void ShowLosingPanel() =>
        SwitchPanelVisibility(true, 0f);

    private void ReloadScene()
    {
        SwitchPanelVisibility(false, 1f);
        SceneManager.LoadScene(0);
    }

    private void SwitchPanelVisibility(bool isActive, float timeScale)
    {
        _losingPanel.SetActive(isActive);
        Time.timeScale = timeScale;

        if (isActive)
        {
            UFO[] arrayLiveUFOs = FindObjectsOfType<UFO>();

            if (arrayLiveUFOs != null)
                foreach (var ufo in arrayLiveUFOs)
                    ufo.GetComponent<AudioSource>().mute = true;
        }
    }
}
