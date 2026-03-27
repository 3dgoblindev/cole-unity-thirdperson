using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    CharacterController characterController;
    GameObject playerModel;
    Animator animator;  

    InputAction moveAction;
    InputAction jumpAction;
    InputAction SprintAction;
    InputAction interactAction;

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
        //Gets the child of the player with the name PlayerModel
        playerModel = transform.Find("PlayerModel").gameObject;
        animator = playerModel.GetComponent<Animator>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        SprintAction = InputSystem.actions.FindAction("Sprint");
        interactAction = InputSystem.actions.FindAction("Interact");

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        CalculateGravity();
        Jump();
        Dance();
        Sprint();
        MovePlayer();
        CalculateStamina();
        TurnPlayer();
        UpdateAnimations();
    }
    void UpdateAnimations() 
    {
        animator.SetFloat("Speed", characterController.velocity.magnitude);
        //print("Speed: " + characterController.velocity.magnitude);
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
            stamina -= 4f*staminaFactor;
            animator.SetTrigger("Jump");
        }
        
    }

    void Dance()
    { 
        if (interactAction.triggered)
        {
            animator.SetTrigger("Dance");
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
                stamina += Time.deltaTime * staminaFactor * 2f;
            }
            else
            {
                stamina += Time.deltaTime * staminaFactor * 4f;

            }
        }
        stamina = Mathf.Clamp(stamina, 0, 1);
        staminaBar.localScale = new Vector3(stamina, 1, 1);
    }
}
