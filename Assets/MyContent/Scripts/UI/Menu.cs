using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public event UnityAction<int> SwitchingControl;

    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonNewGame;
    [SerializeField] private Button _buttonControl;
    [SerializeField] private Button _buttonQuit;
    [SerializeField] private Text _typeControlDisplay;

    private static bool _isActiveMenu = true;
    private static int _typeControl = 1;

    private bool _isActiveButtonContinue;

    private void Start()
    {
        _buttonNewGame.onClick.AddListener(ReloadScene);
        _buttonContinue.onClick.AddListener(ContinueGame);
        _buttonControl.onClick.AddListener(SwitchControl);
        _buttonQuit.onClick.AddListener(Quit);
        _panelMenu.SetActive(_isActiveMenu);

        if (!_isActiveMenu)
            _isActiveButtonContinue = true;

        _buttonContinue.interactable = _isActiveButtonContinue;

        //_typeControlDisplay.text = _typeControl == 1 ? "Keyboard" : "Keyboard + mouse";

        Time.timeScale = _isActiveMenu ? 0f : 1f;
    }

    private void Update()
    {
        if(!_isActiveMenu)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isActiveMenu = !_isActiveMenu;
                _panelMenu.SetActive(_isActiveMenu);
                Time.timeScale = 0f;
            }
    }

    private void ContinueGame()
    {
        _isActiveMenu = !_isActiveMenu;
        _panelMenu.SetActive(_isActiveMenu);
        Time.timeScale = 1f;
    }

    private void ReloadScene()
    {
        _isActiveMenu = false;
        SceneManager.LoadScene(0);
    }

    private void SwitchControl()
    {
        if (_typeControl == 1)
        {
            _typeControl = 2;
            _typeControlDisplay.text = "Keyboard + mouse";
        }

        if (_typeControl == 2)
        {
            _typeControl = 1;
            _typeControlDisplay.text = "Keyboard";
        }
        SwitchingControl?.Invoke(PlayerPrefs.GetInt("TypeControl"));
    }

    private void Quit() => 
        Application.Quit();
}
