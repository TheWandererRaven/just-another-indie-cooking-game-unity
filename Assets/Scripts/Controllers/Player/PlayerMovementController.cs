using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Controller Configuration
    public CameraController playerCameraController;
    public float walkingSpeedUnitsPerSecond = 1.0f;
    public float walkingSpeedMax = 1.0f;
    public float crouchingSpeedUnitsPerSecond = 0.5f;
    public float crouchingSize = 0.5f;
    #endregion
    
    #region Script only variables
    private bool freezeMovment = false;
    private bool freezeCamera = false;
    private float walkingSpeedBase {
        get {
            return ((
                isCrouching ? crouchingSpeedUnitsPerSecond : walkingSpeedUnitsPerSecond
                )/5) * Time.deltaTime;
        }
    }
    private Vector3 walkingInputs = new Vector3(0, 0, 0);
    private Vector2 lookingInputs = new Vector2(0, 0);
    private float standingSize = 1f;
    private float standingRadius = 1.5f;
    private bool isCrouching = false;
    #endregion
    
    #region GameObject Events
    void Start()
    {
        CharacterController playerController = this.GetComponent<CharacterController>();
        standingSize = playerController.height;
        standingRadius = playerController.radius;
    }

    void Update()
    {
        // ##################################### WALKING MOVEMENT #####################################
        if(!freezeMovment) this.GetComponent<CharacterController>().SimpleMove(CalculateMovementSpeed());

        // ##################################### LOOK MOVEMENT #####################################
        if(!freezeCamera) {
            if(Cursor.lockState != CursorLockMode.None) RotateCamera(lookingInputs.x, lookingInputs.y * -1);
        }

        /*
        // ##################################### INTERACT HANDLER ########################g#############
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
    #endregion
    
    #region Auxiliary Methods
    private Vector3 CalculateMovementSpeed() {
        Vector3 Speed = (walkingSpeedBase * 1000) * (
            (this.transform.forward * walkingInputs.z) +
            (this.transform.right * walkingInputs.x)
        );
        if(Speed.magnitude > walkingSpeedMax) Speed = walkingSpeedMax * Speed / Speed.magnitude;
        return Speed;
    }
    #endregion
    
    #region Movement Executors
    public void UpdateWalkingInputs(Vector2 newWalkignInputs) {
        walkingInputs.x = newWalkignInputs.x;
        walkingInputs.z = newWalkignInputs.y;
    }
    public void UpdateLookingInputs(Vector2 newLookingInputs) {
        lookingInputs.x = newLookingInputs.x;
        lookingInputs.y = newLookingInputs.y;
    }
    public void ToggleCrouching(bool crouch) {
        CharacterController playerController = this.GetComponent<CharacterController>();
        isCrouching = crouch;
        playerController.height = isCrouching ? crouchingSize : standingSize;
        playerController.center = new Vector3(playerController.center.x, 
        isCrouching ? crouchingSize/2 : standingSize/2,
        playerController.center.z);

        if(isCrouching && playerController.radius > crouchingSize/2) playerController.radius = crouchingSize/2;
        else if(playerController.radius != standingRadius) playerController.radius = standingRadius;

        playerCameraController.Crouch(isCrouching ? crouchingSize : standingSize);
    }
    public void RotateCamera(float horizontalRotation, float verticalRotation) {
        // For Horizontal rotation, rotate the player
        this.transform.Rotate(new Vector3(0, horizontalRotation, 0), Space.Self);
        // For Vertical rotation, rotate the camera
        playerCameraController.RotateVertically(verticalRotation);
    }
    #endregion
}
