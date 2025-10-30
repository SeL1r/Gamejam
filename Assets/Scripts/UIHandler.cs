using UnityEngine;

[CreateAssetMenu(fileName = "UIHandler", menuName = "Scriptable Objects/UIHandler")]
public class UIHandler : ScriptableObject
{
    public GameObject currentHintUI;

    public void SetHintUI(GameObject hintUI)
    {
        currentHintUI = hintUI;
    }
    public void ShowHint()
    {
        currentHintUI.SetActive(true);
    }

    public void HideHint()
    {
        currentHintUI.SetActive(false);
    }
}
