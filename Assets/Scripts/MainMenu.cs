using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button play, settings, exit;

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
}
