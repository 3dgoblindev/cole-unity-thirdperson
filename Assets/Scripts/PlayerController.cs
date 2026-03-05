using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    CharacterController characterController;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction SprintAction;

    Vector2 moveVector;

    public float speed = 5;
    public float sprintSpeed = 10;
    public float walkSpeed = 5;


    public float gravity = 9.81f;

    public float verticalForce = 0;

    public float jumpHeight = 0.1f;

    public float stamina = 1;
    public float staminaFactor = 0.2f;

    bool isSprinting = false;

    public RectTransform staminaBar;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        SprintAction = InputSystem.actions.FindAction("Sprint");

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        CalculateGravity();
        Jump();
        Sprint();
        MovePlayer();
        CalculateStamina();
        TurnPlayer();
    }

    void UpdateInput() {
        moveVector = moveAction.ReadValue<Vector2>();
    }

    void MovePlayer()
    {
        Vector3 move = new Vector3(moveVector.x, 0, moveVector.y);
        move = Camera.main.transform.TransformDirection(move);

        move.y = verticalForce;
        characterController.Move(move * Time.deltaTime * speed);

        
    }

    void CalculateGravity() 
    {
        if (characterController.isGrounded)
        {
            verticalForce = -1;
        }
        else         
        {
            verticalForce += gravity * Time.deltaTime;
        }
    }

    void TurnPlayer()
    {
        if (Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.y) > 0) {

            Vector3 currentLookRotation = characterController.velocity.normalized;
            currentLookRotation.y = 0;
            currentLookRotation.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(currentLookRotation);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

        }

    }

    void Jump() 
    {
        if(characterController.isGrounded && jumpAction.triggered && stamina >= 0.2f)
        {
            verticalForce = Mathf.Sqrt(-2 * gravity * jumpHeight);
            print("saltando");
            stamina -= 0.2f;
        }
    }

    void Sprint() 
    {
        if (SprintAction.IsPressed() && characterController.isGrounded && stamina >= 0.05f) 
        {
            speed = sprintSpeed;
            isSprinting = true;
        }
        else
        {
            speed = walkSpeed;
            isSprinting = false;
        }
    }

    void CalculateStamina()
    {
        if (SprintAction.IsPressed() && characterController.velocity.normalized.magnitude > 0)
        {
            stamina -= Time.deltaTime * staminaFactor;

        }
        else {
            if (!SprintAction.IsPressed() && characterController.velocity.normalized.magnitude > 0)
            {
                stamina += Time.deltaTime * staminaFactor * 0.2f;
            }
            else
            {
                stamina += Time.deltaTime * staminaFactor * 0.8f;

            }
        }
        stamina = Mathf.Clamp(stamina, 0, 1);
        staminaBar.localScale = new Vector3(stamina, 1, 1);
    }
}
