using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    CharacterController characterController;

    InputAction moveAction;

    Vector2 moveVector;

    public float speed = 5;

    public float gravity = 9.81f;

    public float verticalForce = 0;
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateGravity();
        UpdateInput();
        MovePlayer();
    }

    void UpdateInput() {
        moveVector = moveAction.ReadValue<Vector2>();

    }

    void MovePlayer()
    {
        Vector3 move = new Vector3(moveVector.x, 0, moveVector.y);
        move.y = verticalForce;
        characterController.Move(move * Time.deltaTime * speed);
    }

    void CalculateGravity() 
    {
        if (characterController.isGrounded)
        {
            verticalForce = -1;
        }
        else         {
            verticalForce += gravity * Time.deltaTime;
        }
    }
}
