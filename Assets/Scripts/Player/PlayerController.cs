using System;
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
    private bool isActive = true;
    [SerializeField] private AudioClip engineClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float pitch;



    private void Awake()
    {

        playerInput = GetComponent<PlayerInput>();
        weaponManager = GetComponent<WeaponManager>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        audioSource.clip = engineClip;
        audioSource.Play();

    }
    private void OnEnable()
    {
        StaticEventHandler.OnGameOver += StaticEventHandler_OnGameOver;
        StaticEventHandler.OnGameWon += StaticEventHandler_OnGameWon;
    }



    private void OnDisable()
    {
        StaticEventHandler.OnGameOver -= StaticEventHandler_OnGameOver;
        StaticEventHandler.OnGameWon -= StaticEventHandler_OnGameWon;
    }


    private void Update()
    {
        if (!isActive) { return; }
        HandleMovement();
        HandleSound();

    }

    private void HandleSound()
    {
        float enginePitch = Mathf.Lerp(pitch, pitch * 2, (moveSpeed - minSpeed) / (maxSpeed - minSpeed));
        audioSource.pitch = enginePitch;
    }

    private void StaticEventHandler_OnGameWon(GameWonArgs obj)
    {
        isActive = false;
        weaponManager.isActive = false;
        audioSource.Stop();
    }

    private void StaticEventHandler_OnGameOver(GameOverArgs obj)
    {
        isActive = false;
        weaponManager.isActive = false;
        audioSource.Stop();
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


    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            //Debug.Log("Fire: " + context.phase);
        }
    }
    public float GetSpeed()
    {
        return moveSpeed;
    }


}
