using UnityEngine;
using UnityEngine.InputSystem;

public class MovementCharacter : MonoBehaviour
{
	float maxHeadAngle;
	float sensetivity;
	public Camera mainCamera;
	public GameObject head;
	InputAction moveAction, jumpAction, sprintAction, lookAction;
	Rigidbody rb;
	float speedCharacter = 3, maxForceJump = 7;
	void Start()
	{
		//Выгруженние сохранений
		sensetivity = PlayerPrefs.GetFloat("Sensetivity");
		
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
		Vector3 localVelocity = new Vector3(move.x * speedCharacter, rb.linearVelocity.y, move.y * speedCharacter);
		Vector3 globalVelocity = transform.TransformDirection(localVelocity);
		rb.linearVelocity = globalVelocity;
	}
	
	void Update()
	{
		//Пускаем луч из центра
		Vector3 centerPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
		Ray ray = mainCamera.ScreenPointToRay(centerPoint);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit))
		{
			GameObject hitObject = hit.collider.gameObject;
		}

		//Поворот камеры
		Vector2 look = lookAction.ReadValue<Vector2>() * Time.deltaTime * sensetivity;
		maxHeadAngle = Mathf.Clamp(maxHeadAngle - look.y, -70, 55);
		if (look != Vector2.zero)
		{
			head.transform.localRotation = Quaternion.Euler(maxHeadAngle, 0, 0);
			transform.Rotate(0, look.x, 0);
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