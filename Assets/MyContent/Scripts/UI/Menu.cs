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
        Time.timeScale = _isActiveMenu ? 0f : 1f;
        _typeControlDisplay.text = _typeControl == 1 ? "Keyboard" : "Keyboard + mouse";
        SwitchingControl?.Invoke(_typeControl);
    }

    private void Update()
    {
        if(!_isActiveMenu)
            if (Input.GetKeyDown(KeyCode.Escape))
                ShowMenu(true, 0f);    
    }

    private void ContinueGame() =>
        ShowMenu(false, 1f);
    

    private void ReloadScene()
    {
        _isActiveMenu = false;
        SceneManager.LoadScene(0);
    }

    private void SwitchControl()
    {
        if (_typeControl == 1)
            SetTypeControlAndTextButton(2, "Keyboard + mouse");
        
        else
            SetTypeControlAndTextButton(1, "Keyboard");
        
        SwitchingControl?.Invoke(_typeControl);
    }

    private void Quit() => 
        Application.Quit();

    private void SetTypeControlAndTextButton(int type, string text)
    {
        _typeControl = type;
        _typeControlDisplay.text = text;
    }

    private void ShowMenu(bool isActive, float timeScale)
    {
        _panelMenu.SetActive(isActive);
        Time.timeScale = timeScale;
    }
}
