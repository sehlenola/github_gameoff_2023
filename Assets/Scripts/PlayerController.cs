using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += Fire;
        playerInputActions.Player.Movement.performed += Movement_performed;
        //playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    private void Update()
    {


        HandleMovement();

    }

    private void HandleMovement()
    {
        // Constant forward movement
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Rotation based on input
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Debug.Log(inputVector);

        float rotation = inputVector.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            //Debug.Log("Fire: " + context.phase);
        }
    }



}
