using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MovementCharacter : MonoBehaviour
{
	public UIHandler UIhandle;
	int _index = 0;
	float maxHeadAngle;
	float sensetivity;
	public Data data;
    public GameObject hitObject;
    public Camera mainCamera;
	public GameObject head, hintUI;
	InputAction moveAction, jumpAction, sprintAction, lookAction, interactAction;
	Rigidbody rb;
	float speedCharacter = 1, maxForceJump = 7;
	void Start()
	{
		UIhandle.SetHintUI(hintUI);
		
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
        if (interactAction.WasPressedThisFrame() && hitObject != null)
        {
            switch (hitObject.tag)
            {
                case "Radio":
                    TextMeshPro tmp = hitObject.GetComponent<TextMeshPro>();

                    if (tmp != null)
                    {
                        data.NextSubtitle(tmp, _index);
                        _index++;
                        break;
                    }
                    Debug.Log("TMP not found");
                    break;
                default:
                    break;
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
}