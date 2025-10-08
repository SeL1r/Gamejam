            using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private InputAction pauseAction;
    public GameObject menuPanel;
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;

    private bool _isPaused = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseAction = InputSystem.actions.FindAction("Pause");
        InitializeButtons();
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            TogglePause();
        }
    }

    private void InitializeButtons()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);

        menuPanel.SetActive(false); 
    }

    void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            menuPanel.SetActive(true);
            Time.timeScale = 0;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    void OnPlayButtonClick()
    { 
        TogglePause();
    }

    void OnSettingsButtonClick()
    {

    }

    void OnExitButtonClick()
    {
        TogglePause();
        SceneManager.LoadScene("Menu");
    }
}
