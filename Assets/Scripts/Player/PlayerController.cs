using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public float jumpStaminaCost;
    public float dashSpeed;
    public float dashStaminaCost;
    private float finalSpeed;
    private bool isDashing = false;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public float additionalMoveSpeed;

    private bool isWalled;
    private bool isMoving;

    [HideInInspector] public event Action<bool> onMove;
    [HideInInspector] public event Action<bool> onDash;
    [HideInInspector] public event Action onJump;

    private Rigidbody playerRb;
    private PlayerStatus playerStatus;

    private Coroutine coroutine;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        finalSpeed = moveSpeed;
    }

    private void Update()
    {
        if (isDashing)
        {
            playerStatus.stats[(int)PlayerStatus.StatusType.STAMINA].Add(-dashStaminaCost * Time.deltaTime);

            if(playerStatus.stats[(int)PlayerStatus.StatusType.STAMINA].curValue <= 0)
            {
                finalSpeed = moveSpeed;
                isDashing = false;
                onDash.Invoke(false);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        IsGrounded();
        IsWalled();
    }

    private void Move()
    {
        if (!isMoving) return;

        Vector3 dir;
        if (isWalled)
        {
            dir = transform.up * curMovementInput.y + transform.right * curMovementInput.x;
        }
        else
        {
            dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

        }

        dir = dir.normalized;
        dir *= finalSpeed + additionalMoveSpeed;

        if (!isWalled)
        {
            dir.y = playerRb.velocity.y;
        }

        playerRb.velocity = dir;
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            onMove.Invoke(true);
            isMoving = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;

            playerRb.velocity = Vector2.zero;
            onMove.Invoke(false);
            isMoving = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            if(playerStatus.stats[(int)PlayerStatus.StatusType.STAMINA].curValue > 0)
            {
                playerRb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                playerStatus.stats[(int)PlayerStatus.StatusType.STAMINA].Add(-jumpStaminaCost);
                onJump.Invoke();
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsGrounded())
        {
            if (playerStatus.stats[(int)PlayerStatus.StatusType.STAMINA].curValue > 0)
            {
                finalSpeed = moveSpeed + dashSpeed;
                isDashing = true;
                onDash.Invoke(true);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            finalSpeed = moveSpeed;
            isDashing = false;
            onDash.Invoke(false);
        }
    }

    public void OnStepPlatform(Vector3 direction, float power)
    {
        playerRb.AddForce(direction * power, ForceMode.Impulse);
        onJump.Invoke();
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], out RaycastHit hit, 0.1f, groundLayerMask))
            {
                transform.SetParent(hit.collider.transform);
                return true;
            }
        }

        transform.SetParent(null);
        return false;
    }

    private bool IsWalled()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.1f) + (transform.right * 0.2f) + (transform.up * 0.3f), transform.TransformDirection(Vector3.forward)),
            new Ray(transform.position + (transform.forward * 0.1f) + (-transform.right * 0.2f) + (transform.up), transform.TransformDirection(Vector3.forward)),
            new Ray(transform.position + (transform.forward * 0.1f) + (transform.right * 0.2f) + (transform.up), transform.TransformDirection(Vector3.forward)),
            new Ray(transform.position + (transform.forward * 0.1f) + (-transform.right * 0.2f) + (transform.up * 0.3f), transform.TransformDirection(Vector3.forward)),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, groundLayerMask))
            {
                isWalled = true;
                return true;
            }
        }

        isWalled = false;
        return false;
    }

    public new void StartCoroutine(IEnumerator coroutine)
    {
        this.coroutine ??= base.StartCoroutine(coroutine);
    }

    public void StopCoroutine()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
