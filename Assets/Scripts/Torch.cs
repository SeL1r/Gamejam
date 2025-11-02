using UnityEngine;
using UnityEngine.InputSystem;
public class Torch : MonoBehaviour
{
	public GameObject lightTorch;
	public InputAction switchTorch;
	public AudioSource audioSource;
	public static bool isTorch = false;

    private void Start()
    {
		switchTorch = InputSystem.actions.FindAction("LightSwitch");
    }
    void Update()
	{
		if (switchTorch.WasPressedThisFrame())
		{
			audioSource.Play();
			if (isTorch)
			{
				lightTorch.SetActive(false);
				isTorch = false;
			}
			else
			{
				lightTorch.SetActive(true);
				isTorch = true;
			}
		}
	}
}
