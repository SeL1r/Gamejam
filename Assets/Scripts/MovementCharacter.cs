using UnityEngine;
using UnityEngine.InputSystem;

public class MovementCharacter : MonoBehaviour
{
	public GameObject head;
	InputAction moveAction, jumpAction, sprintAction, lookAction;
	Rigidbody rb;
	float speedCharacter = 3, maxForceJump = 10;
	void Start()
	{
		//Задаю переменный от сюда
		moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");
		sprintAction = InputSystem.actions.FindAction("Sprint");
		lookAction = InputSystem.actions.FindAction("Look");
		
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
		//Поворот камеры
		Vector2 look = lookAction.ReadValue<Vector2>() * Time.deltaTime;
		Vector2 lookHead = new Vector2(-look.y, 0);
		Vector2 lookPlayer = new Vector2(0, look.x);
		if (look != Vector2.zero)
		{
			head.transform.Rotate(lookHead);
			transform.Rotate(lookPlayer);
		}
		
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
			
			if(jumpAction.IsPressed())
			{
				rb.AddForce(Vector3.up * maxForceJump, ForceMode.Impulse);
			}
		}
	}
}