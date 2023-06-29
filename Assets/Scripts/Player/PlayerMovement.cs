using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private PlayerInputActions input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 smoothedMovementInput = Vector2.zero;
    private Vector2 movementInputSmoothVelocity = Vector2.zero;
    private Rigidbody2D rb = null;
    private PlayerStats stats;
    [SerializeField] Transform arm;

    // Make move speed as a value in the STATS script
    [SerializeField] private float moveSpeed = 10f;


    private Vector3 mousePosition = Vector3.zero;

    private void Awake()
    {
        input = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        stats = PlayerStats.Instance;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;

    }

    private void Update()
    {
        mousePosition = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateTowardMouse();
        if (moveVector.x != 0 || moveVector.y != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else { animator.SetBool("isRunning", false); }
    }

    private void RotateTowardMouse()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        arm.up = -direction;
    }

    private void SetPlayerVelocity()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, moveVector, ref movementInputSmoothVelocity, 0.1f);
        rb.velocity = smoothedMovementInput * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();

    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;

    }
}
