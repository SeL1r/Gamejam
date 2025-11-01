using UnityEngine;
public class Torch : MonoBehaviour
{
	public GameObject lightTorch;
	public static bool isTorch = false;
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
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
