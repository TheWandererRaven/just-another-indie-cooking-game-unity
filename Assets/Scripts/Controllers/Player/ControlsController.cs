using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsController : MonoBehaviour
{
    public ConfigurationController config;
    public MovementController playerMovement;
    public CharacterController playerController;
    public CameraController cameraController;
    public float walkingSpeedUnitsPerSecond = 1.0f;
    private float walkingSpeedBase {
        get {
            return walkingSpeedUnitsPerSecond * Time.deltaTime;
        }
    }
    private Vector3 calculateAxisDirection(Vector3 Axis, float direction) {
        return direction * (walkingSpeedBase * 1000) * Axis;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        //if(moveHorizontal != 0f || moveVertical != 0f) playerMovement.moveForcefully(moveHorizontal, moveVertical);
        playerController.SimpleMove(calculateAxisDirection(this.transform.forward, moveVertical) +
            calculateAxisDirection(this.transform.right, moveHorizontal));
        
        /* No need for speed multiplier, change sensitivity on the Input config
        if(rotateHorizontal != 0f) playerMovement.rotateDirectionally(rotateHorizontal * config.lookSpeedMultiplier, 0);
        if(rotateVertical != 0f) cameraMovement.rotateDirectionally(0, rotateVertical * config.lookSpeedMultiplier * -1);
        */
        if(rotateHorizontal != 0f && Cursor.lockState != CursorLockMode.None) playerMovement.rotateDirectionally(rotateHorizontal, 0);
        if(rotateVertical != 0f && Cursor.lockState != CursorLockMode.None) cameraController.rotateVertically(rotateVertical * -1);
        if(Input.GetKeyDown(KeyCode.Escape)) if(Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None; else Application.Quit();
        if(Input.GetMouseButton(0)){
            if(Cursor.lockState != CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Locked;
            }
    }
}
