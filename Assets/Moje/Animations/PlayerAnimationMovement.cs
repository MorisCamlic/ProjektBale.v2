using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr : MonoBehaviour
{
    PlayerInput PlayerInput;
    CharacterController CharacterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 10.0f;
    float moveSpeed = 5.0f; // Uncommented and initialized moveSpeed
    float runMultiplier = 1.5f; // Adjusted runMultiplier to desired value
    float speedMultiplier = 1.5f; // Initialized speedMultiplier
    
    AudioSource walkingAudioSource;

    void Awake()
    {
        PlayerInput = new PlayerInput();
        CharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        walkingAudioSource = GetComponent<AudioSource>();

        PlayerInput.CharacterControls.Move.started += onMovementInput;
        PlayerInput.CharacterControls.Move.canceled += onMovementInput;
        PlayerInput.CharacterControls.Move.performed += onMovementInput;
        PlayerInput.CharacterControls.Run.started += onRun;
        PlayerInput.CharacterControls.Run.canceled += onRun;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * moveSpeed; // Apply moveSpeed
        currentMovement.z = currentMovementInput.y * moveSpeed; // Apply moveSpeed
        currentRunMovement.x = currentMovementInput.x * moveSpeed * runMultiplier; // Apply moveSpeed and runMultiplier
        currentRunMovement.z = currentMovementInput.y * moveSpeed * runMultiplier; // Apply moveSpeed and runMultiplier
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;

        if (isMovementPressed && !walkingAudioSource.isPlaying)
        {
            walkingAudioSource.Play();
        }
        else if (!isMovementPressed && walkingAudioSource.isPlaying)
        {
            walkingAudioSource.Stop();
        }
    }

    void handleAnimation()
    {
        if (isMovementPressed && isRunPressed)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
        }
        else if (isMovementPressed)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", false);
        }
        else if (!isMovementPressed)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }
    }

    void handleGravity()
    {
        if (CharacterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }

    void Update()
    {
        handleRotation();
        handleAnimation();
        handleGravity();

        if (isRunPressed)
        {
            CharacterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            CharacterController.Move(currentMovement * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        PlayerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        PlayerInput.CharacterControls.Disable();
    }
}
