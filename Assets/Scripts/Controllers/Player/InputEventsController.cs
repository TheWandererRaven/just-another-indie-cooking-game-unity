using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEventsController : MonoBehaviour
{
    public float MouseSensitivity = 10;
    public PlayerMovementController playerMovementController;
    public InteractionController playerInteractionController;
    public PlayerUIDisplayManagerController playerUIDisplayManagerController;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(InputAction.CallbackContext context) {
        if(context.performed) {
            playerMovementController.UpdateWalkingInputs(context.ReadValue<Vector2>());
        } else if(context.canceled) {
            playerMovementController.UpdateWalkingInputs(Vector2.zero);
        }
    }
    public void Look(InputAction.CallbackContext context) {
        if(context.performed) {
            playerMovementController.UpdateLookingInputs(context.ReadValue<Vector2>() / 10);
            //lookingInputs.x = lookingMovement.x/10;
        } else if(context.canceled) {
            playerMovementController.UpdateLookingInputs(Vector2.zero);
        }
    }
    public void Crouch(InputAction.CallbackContext context) {
        playerMovementController.ToggleCrouching(context.performed);
    }
    public void handPrimaryAction(InputAction.CallbackContext context) {
        playerInteractionController.executePrimaryAction(context.phase);
    }
    public void handSecondaryAction(InputAction.CallbackContext context) {
        playerInteractionController.executeSecondaryAction(context.phase);
    }
    public void Interact(InputAction.CallbackContext context) {
        if(context.canceled) {
            playerInteractionController.interactWithObjectInSights();
        }
    }
    public void Grab(InputAction.CallbackContext context) {
        if(!playerInteractionController.hasGrabbedObject() && context.performed)
            playerInteractionController.grabObjectInSights();
        else if(context.canceled)
            if(playerInteractionController.grabbedObject != null) playerInteractionController.dropGrabbedObject();
    }
    public void toggleInventoryScreen(InputAction.CallbackContext context) {
        if(context.canceled) {
            if(playerUIDisplayManagerController.IsDisplayActive("InventoryDisplay")) {
                playerUIDisplayManagerController.changeDisplay("MainDisplay");
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                playerUIDisplayManagerController.changeDisplay("InventoryDisplay");
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }
}
