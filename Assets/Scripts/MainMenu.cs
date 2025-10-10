using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button play, settings, exit, apply, cancel;

    void Start()
    {
        ButtonInstance();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void ButtonInstance()
    {
        play.onClick.AddListener(Play);
        settings.onClick.AddListener(OpenSettings);
        exit.onClick.AddListener(Exit);
        apply.onClick.AddListener(CloseSettings);
        cancel.onClick.AddListener(CloseSettings);
    }
    void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Exit()
    {
        #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
