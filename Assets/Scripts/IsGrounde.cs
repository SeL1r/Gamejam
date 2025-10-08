using System;
using UnityEngine;

public class IsGrounde : MonoBehaviour
{
	public static bool isGrounded = false;

	void OnTriggerEnter(Collider other)
	{
		isGrounded = true;
	}

    void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
