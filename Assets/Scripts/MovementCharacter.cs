using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.AI;

public class MovementCharacter : MonoBehaviour
{
	public UIHandler UIhandle;
	float maxHeadAngle;
	float sensetivity;
	private Animator anim;
	public Data data;
	public GameObject hitObject, pistol;
	public Camera mainCamera;
	public GameObject head, hintUI;
	InputAction moveAction, jumpAction, sprintAction, lookAction, interactAction;
	Rigidbody rb;
	float speedCharacter = 1, maxForceJump = 7;
	public AudioSource radioSource, doorSource, doorSource2, safeSource, playerSource;
	private bool isSoundsDoor = true, isSoundsSafe = true;
	void Start()
	{
		UIhandle.SetHintUI(hintUI);
		playerSource = GetComponent<AudioSource>();
		moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");
		sprintAction = InputSystem.actions.FindAction("Sprint");
		lookAction = InputSystem.actions.FindAction("Look");
		interactAction = InputSystem.actions.FindAction("Interact");
		
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		
		Vector2 move = moveAction.ReadValue<Vector2>();
		Vector3 localVelocity = new Vector3(move.x * speedCharacter, rb.linearVelocity.y, move.y * speedCharacter);
		Vector3 globalVelocity = transform.TransformDirection(localVelocity);
		rb.linearVelocity = globalVelocity;
		if ((Mathf.Abs(rb.linearVelocity.x) > .05f || Mathf.Abs(rb.linearVelocity.z) > .05f) && !playerSource.isPlaying)
		{
			playerSource.Play();
		}
		else if (Mathf.Abs(rb.linearVelocity.x) < .05f || Mathf.Abs(rb.linearVelocity.z) < .05f)
		{
			
			playerSource.Stop();
		}
	}
	
	void Update()
	{
		
		Vector3 centerPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
		Ray ray = mainCamera.ScreenPointToRay(centerPoint);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1f))
		{
			hitObject = hit.collider.gameObject;
		}
		
		//Shadow back
		NavMeshAgent agent = hitObject.GetComponent<NavMeshAgent>();
		if (Torch.isTorch)
        {
        	agent.speed = -5;
        }
		
		if (interactAction.WasPressedThisFrame() && hitObject != null)
		{
			switch (hitObject.tag)
			{
				
				case "Radio":
					TextMeshPro tmp = hitObject.GetComponent<TextMeshPro>();

					if (tmp != null)
					{
						data.NextSubtitle(tmp, radioSource);
						break;
					}
					Debug.Log("TMP not found");
					break;
				case "Door":
					anim = hitObject.GetComponent<Animator>();
					if (isSoundsDoor && data.readyToOpen)
					{
						anim.SetBool("IsOpen", !anim.GetBool("IsOpen"));
						doorSource.Play();
						isSoundsDoor = false;
						hitObject.layer = 0;
						break;
					}
					doorSource2.Play();
					break;

				case "Shadow":
					NavMeshAgent agent = hitObject.GetComponent<NavMeshAgent>();
					if (Torch.isTorch)
                    {
                        agent.speed = -5;
                    }
					break;
				case "Safe":
					anim = hitObject.GetComponent <Animator>();
					if (data.readyToOpen && isSoundsSafe)
					{
						anim.SetBool("Open", true);
						safeSource.Play();
						isSoundsSafe = false;
						hitObject.layer = 2;
					}
					break;
				case "LevelUp":
					data.NextStage();
					data.SwichIsReadyToOpen();
					Destroy(hitObject);
					break;
				case "Pistol":
					pistol.SetActive(true);
					break;
                default:
                    break;
            }

			}

		}
		switch (hitObject.layer)
		{
			case 6:
				if (UIhandle.currentHintUI.activeSelf)
					break;
				UIhandle.ShowHint();
				break;
			default:
				if (UIhandle.currentHintUI.activeSelf)
				{
					UIhandle.HideHint();
				}
				hitObject = head;
				break;
		}


		sensetivity = PlayerPrefs.GetFloat("Sensetivity");
		Vector2 look = lookAction.ReadValue<Vector2>() * Time.deltaTime * sensetivity;
		maxHeadAngle = Mathf.Clamp(maxHeadAngle - look.y, -70, 55);
		if (look != Vector2.zero)
		{
			head.transform.localRotation = Quaternion.Euler(maxHeadAngle, 0, 0);
			transform.Rotate(0, look.x, 0);
		}
		
		
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
