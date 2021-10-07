using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsController : MonoBehaviour
{
    #region Controller Configuration
    public ConfigurationController config;
    public MovementController playerMovement;
    public CharacterController playerController;
    public CameraController cameraController;
    public InteractionController interactionController;
    public GameObject handObject;
    public float walkingSpeedUnitsPerSecond = 1.0f;
    public float walkingSpeedMax = 1.0f;
    public float crouchingSpeedUnitsPerSecond = 0.5f;
    public float crouchingSize = 0.5f;
    #endregion
    #region Script only variables
    private float walkingSpeedBase {
        get {
            return ((
                isCrouching ? crouchingSpeedUnitsPerSecond : walkingSpeedUnitsPerSecond
                )/5) * Time.deltaTime;
        }
    }
    private float standingSize = 1f;
    private float standingRadius = 1.5f;
    private bool isCrouching = false;
    private Vector3 walkingInputs = new Vector3(0, 0, 0);
    private Vector2 lookingInputs = new Vector2(0, 0);
    #endregion
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
    #region Helper Methods
    private Vector3 calculateMovementSpeed() {
        Vector3 Speed = (walkingSpeedBase * 1000) * (
            (this.transform.forward *  walkingInputs.z) +
            (this.transform.right *  walkingInputs.x)
        );
        if(Speed.magnitude > walkingSpeedMax) Speed = walkingSpeedMax * Speed / Speed.magnitude;
        print(Speed.magnitude);
        return Speed;
    }
    #endregion
    #region Input Event Handlers
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
    public void Interact1(InputAction.CallbackContext context) {

    }
    public void Interact2(InputAction.CallbackContext context) {

    }
    public void Interact3(InputAction.CallbackContext context) {
        if(context.canceled) {
            interactionController.interactWithObjectInSights();
        }
    }
    public void Grab(InputAction.CallbackContext context) {
        if(!interactionController.hasGrabbedObject() && context.performed)
            interactionController.grabObjectInSights();
        else if(context.canceled)
            if(interactionController.grabbedObject != null) interactionController.dropGrabbedObject();
    }
    #endregion
}
