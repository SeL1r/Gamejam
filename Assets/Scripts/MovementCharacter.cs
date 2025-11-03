using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.AI;

public class MovementCharacter : MonoBehaviour
{
    [Header("References")]
    public UIHandler UIhandle;
    public Data data;
    public GameObject hitObject, pistol;
    public Camera mainCamera;
    public GameObject head, hintUI;
    public AudioSource radioSource, doorSource, doorSource2, safeSource, playerSource;

    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float jumpForce = 7f;
    public float sensitivity = 1f;

    [Header("Footstep Settings")]
    public AudioClip[] footstepClips;
    public float footstepDelay = 0.5f;
    public float velocityThreshold = 0.1f;

    private Animator anim;
    private InputAction moveAction, jumpAction, sprintAction, lookAction, interactAction;
    private Rigidbody rb;
    private float maxHeadAngle;
    private float currentSpeed;
    private bool isSoundsDoor = true, isSoundsSafe = true;
    private float lastFootstepTime;
    private Vector3 lastPosition;
    private float distanceMoved;

    void Start()
    {
        UIhandle.SetHintUI(hintUI);
        playerSource = GetComponent<AudioSource>();

        // Initialize Input Actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        lookAction = InputSystem.actions.FindAction("Look");
        interactAction = InputSystem.actions.FindAction("Interact");

        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        lastPosition = transform.position;

        // Load sensitivity from PlayerPrefs
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleFootsteps();
    }

    void Update()
    {
        HandleRaycast();
        HandleInteraction();
        HandleCameraLook();
        HandleSprint();
        HandleJump();
    }

    void HandleMovement()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        Vector3 localVelocity = new Vector3(move.x * currentSpeed, rb.linearVelocity.y, move.y * currentSpeed);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);
        rb.linearVelocity = globalVelocity;
    }

    void HandleFootsteps()
    {
        if (!IsGrounde.isGrounded)
        {
            if (playerSource.isPlaying)
                playerSource.Stop();
            return;
        }

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        bool isMoving = horizontalVelocity.magnitude > velocityThreshold;

        if (isMoving && Time.time - lastFootstepTime > footstepDelay)
        {
            PlayFootstep();
            lastFootstepTime = Time.time;
        }
        else if (!isMoving && playerSource.isPlaying)
        {
            playerSource.Stop();
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0 && IsGrounde.isGrounded)
        {
            AudioClip randomClip = footstepClips[Random.Range(0, footstepClips.Length)];
            playerSource.pitch = Random.Range(0.9f, 1.1f);
            playerSource.volume = Random.Range(0.8f, 1f);

            if (!playerSource.isPlaying)
                playerSource.PlayOneShot(randomClip);
        }
    }

    void HandleRaycast()
    {
        Vector3 centerPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = mainCamera.ScreenPointToRay(centerPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            hitObject = hit.collider.gameObject;
            HandleHintUI(hitObject);
        }
        else
        {
            hitObject = head;
            if (UIhandle.currentHintUI.activeSelf)
            {
                UIhandle.HideHint();
            }
        }
    }

    void HandleHintUI(GameObject targetObject)
    {
        switch (targetObject.layer)
        {
            case 6: // Interactable layer
                if (!UIhandle.currentHintUI.activeSelf)
                    UIhandle.ShowHint();
                break;
            default:
                if (UIhandle.currentHintUI.activeSelf)
                {
                    UIhandle.HideHint();
                }
                break;
        }
    }

    void HandleInteraction()
    {
        if (interactAction.WasPressedThisFrame() && hitObject != null)
        {
            switch (hitObject.tag)
            {
                case "Radio":
                    HandleRadioInteraction();
                    break;
                case "Door":
                    HandleDoorInteraction();
                    break;
                case "Shadow":
                    HandleShadowInteraction();
                    break;
                case "Safe":
                    HandleSafeInteraction();
                    break;
                case "LevelUp":
                    HandleLevelUpInteraction();
                    break;
                case "Pistol":
                    HandlePistolInteraction();
                    break;
                default:
                    break;
            }
        }
    }

    void HandleRadioInteraction()
    {
        TextMeshPro tmp = hitObject.GetComponent<TextMeshPro>();
        if (tmp != null)
        {
            data.NextSubtitle(tmp, radioSource);
        }
        else
        {
            Debug.LogWarning("TMP component not found on Radio object");
        }
    }

    void HandleDoorInteraction()
    {
        if (!data.readyToOpen)
        {
            doorSource2.Play();
            return;
        }

        Animator doorAnim = hitObject.GetComponent<Animator>();
        if (doorAnim != null && isSoundsDoor)
        {
            bool isOpen = !doorAnim.GetBool("IsOpen");
            doorAnim.SetBool("IsOpen", isOpen);
            doorSource.Play();
            isSoundsDoor = false;
            hitObject.layer = 0; // Default layer
        }
    }

    void HandleShadowInteraction()
    {
        NavMeshAgent shadowAgent = hitObject.GetComponent<NavMeshAgent>();
        if (shadowAgent != null && Torch.isTorch)
        {
            shadowAgent.speed = -5;
        }
    }

    void HandleSafeInteraction()
    {
        if (data.readyToOpen && isSoundsSafe)
        {
            Animator safeAnim = hitObject.GetComponent<Animator>();
            if (safeAnim != null)
            {
                safeAnim.SetBool("Open", true);
                safeSource.Play();
                isSoundsSafe = false;
                hitObject.layer = 2; // Ignore Raycast layer
            }
        }
    }

    void HandleLevelUpInteraction()
    {
        data.NextStage();
        data.SwichIsReadyToOpen();
        Destroy(hitObject);
    }

    void HandlePistolInteraction()
    {
        pistol.SetActive(true);
    }

    void HandleCameraLook()
    {
        Vector2 look = lookAction.ReadValue<Vector2>() * Time.deltaTime * sensitivity;

        if (look != Vector2.zero)
        {
            maxHeadAngle = Mathf.Clamp(maxHeadAngle - look.y, -70, 55);
            head.transform.localRotation = Quaternion.Euler(maxHeadAngle, 0, 0);
            transform.Rotate(0, look.x, 0);
        }
    }

    void HandleSprint()
    {
        if (IsGrounde.isGrounded)
        {
            currentSpeed = sprintAction.IsPressed() ? sprintSpeed : walkSpeed;
        }
    }

    void HandleJump()
    {
        if (IsGrounde.isGrounded && jumpAction.WasPressedThisFrame())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Public method to reset door state if needed
    public void ResetDoorState()
    {
        isSoundsDoor = true;
    }

    // Public method to reset safe state if needed
    public void ResetSafeState()
    {
        isSoundsSafe = true;
    }
}
