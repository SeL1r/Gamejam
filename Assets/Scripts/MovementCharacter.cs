using UnityEngine;
using UnityEngine.InputSystem;

public class MovementCharacter : MonoBehaviour
{
	InputAction moveAction, jumpAction, sprintAction;
	Rigidbody rb;
	float speedCharacter = 3, maxForceJump = 10;
	void Start()
	{
		//Задаю переменный от сюда
		moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");
		sprintAction = InputSystem.actions.FindAction("Sprint");
		
		rb = GetComponent<Rigidbody>();
		//Задаю переменные до сюда
	}

	void FixedUpdate()
	{
		//Передвижение игрока
		Vector2 move = moveAction.ReadValue<Vector2>();
		Vector3 velocity = new Vector3(move.x * speedCharacter, rb.linearVelocity.y, move.y * speedCharacter);
		rb.linearVelocity = velocity;
	}
	
	void Update()
	{
		//Прыжок Спринт
		if(IsGrounde.isGrounded)
		{
			if(sprintAction.IsPressed())
			{
				speedCharacter = 6;
			}
			else
			{
				speedCharacter = 3;
			}
			
			if(jumpAction.WasPressedThisFrame())
			{
				rb.AddForce(Vector3.up * maxForceJump, ForceMode.Impulse);
			}
		}
	}
}