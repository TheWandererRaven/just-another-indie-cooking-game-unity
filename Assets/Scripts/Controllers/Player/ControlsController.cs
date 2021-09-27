using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsController : MonoBehaviour
{
    public ConfigurationController config;
    public MovementController playerMovement;
    public CharacterController playerController;
    public CameraController cameraController;
    public float walkingSpeedUnitsPerSecond = 1.0f;
    public float crouchingSpeedUnitsPerSecond = 0.5f;
    private float walkingSpeedBase {
        get {
            return ((
                isCrouching ? crouchingSpeedUnitsPerSecond : walkingSpeedUnitsPerSecond
                )/5) * Time.deltaTime;
        }
    }
    public float crouchingSize = 0.5f;
    private float standingSize = 1f;
    private float standingRadius = 1.5f;
    private bool isCrouching = false;
    public float walkingSpeedMax = 1.0f;
    private Vector3 walkingInputs = new Vector3(0, 0, 0);
    private Vector2 lookingInputs = new Vector2(0, 0);
    private Vector3 calculateMovementSpeed() {
        Vector3 Speed = (walkingSpeedBase * 1000) * (
            (this.transform.forward *  walkingInputs.z) +
            (this.transform.right *  walkingInputs.x)
        );
        Speed.x = (Mathf.Abs(Speed.x) <= walkingSpeedMax) ? Speed.x : (Speed.normalized.x * walkingSpeedMax);
        Speed.z = (Mathf.Abs(Speed.z) <= walkingSpeedMax) ? Speed.z : (Speed.normalized.z * walkingSpeedMax);
        return Speed;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        standingSize = playerController.height;
        standingRadius = playerController.radius;
    }
    void Update()
    {
        // ##################################### WALKING MOVEMENT #####################################
        playerController.SimpleMove(calculateMovementSpeed());

        // ##################################### LOOK MOVEMENT #####################################
        if(Cursor.lockState != CursorLockMode.None) playerMovement.rotateDirectionally(lookingInputs.x, 0);
        if(Cursor.lockState != CursorLockMode.None) cameraController.rotateVertically(lookingInputs.y * -1);

        /*
        // ##################################### INTERACT HANDLER #####################################
        print(Input.GetAxis("Fire1"));
        
        // ##################################### GRAB #####################################
        if(Input.GetMouseButton(0)){
            if(Cursor.lockState != CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Locked;
            }

        // ##################################### MISC INPUT #####################################
        if(Input.GetKeyDown(KeyCode.Escape)) if(Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None; else Application.Quit();
        */
    }
    public void Move(InputAction.CallbackContext context) {
        if(context.performed) {
            Vector2 walkingMovement = context.ReadValue<Vector2>();
            walkingInputs.x = walkingMovement.x;
            walkingInputs.z = walkingMovement.y;
        } else if(context.canceled) {
            walkingInputs.x = 0f;
            walkingInputs.z = 0f;
        }
    }
    public void Look(InputAction.CallbackContext context) {
        if(context.performed) {
            Vector2 lookingMovement = context.ReadValue<Vector2>();
            lookingInputs.x = lookingMovement.x/10;
            lookingInputs.y = lookingMovement.y/10;
        } else if(context.canceled) {
            lookingInputs.x = 0f;
            lookingInputs.y = 0f;
        }
    }
    public void Crouch(InputAction.CallbackContext context) {
        if(context.performed) {
            isCrouching = !isCrouching;
            playerController.height = isCrouching ? crouchingSize : standingSize;
            playerController.center = new Vector3(playerController.center.x, 
            isCrouching ? crouchingSize/2 : standingSize/2,
            playerController.center.z);

            if(isCrouching && playerController.radius > crouchingSize/2) playerController.radius = crouchingSize/2;
            else if(playerController.radius != standingRadius) playerController.radius = standingRadius;

            cameraController.crouch(isCrouching ? crouchingSize : standingSize);
        }
    }
}
