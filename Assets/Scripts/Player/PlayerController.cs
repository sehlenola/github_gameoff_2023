using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private WeaponManager weaponManager;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float minSpeed = 4f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float accelerationRate = 10f;
    [SerializeField] private float rotationSpeed = 200f;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        weaponManager = GetComponent<WeaponManager>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += Fire;
        playerInputActions.Player.Movement.performed += Movement_performed;
        playerInputActions.Player.NextWeapon.performed += NextWeapon_performed;
        playerInputActions.Player.UpgradeWeapon.performed += UpgradeWeapon_performed;
        //playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    private void UpgradeWeapon_performed(InputAction.CallbackContext obj)
    {
        weaponManager.UpgradeWeapon();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Fire.performed -= Fire;
        playerInputActions.Player.Movement.performed -= Movement_performed;
        playerInputActions.Player.NextWeapon.performed -= NextWeapon_performed;
    }

    private void NextWeapon_performed(InputAction.CallbackContext obj)
    {

    }

    private void Update()
    {


        HandleMovement();

    }

    private void HandleMovement()
    {
        // forward movement
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        float acceleration = inputVector.y * accelerationRate * Time.deltaTime;
        moveSpeed = moveSpeed + acceleration;
        moveSpeed = Mathf.Clamp(moveSpeed, minSpeed, maxSpeed);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Rotation

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
