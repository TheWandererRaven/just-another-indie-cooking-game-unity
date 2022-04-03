using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEventsController : MonoBehaviour
{
    #region Controller Configuration
    public float MouseSensitivity = 10;
    public PlayerMovementController playerMovementController;
    public InteractionController playerInteractionController;
    public PlayerHudManager playerHudManager;
    // Start is called before the first frame update
    #endregion
    
    #region Game Object Events
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        
    }
    #endregion

    #region Input Callbacks - MOVEMENT
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
    #endregion
    #region Input Callbacks - INTERACTIONS
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
            if(playerInteractionController.grabbedObject != null) playerInteractionController.releaseGrabbedObject();
    }
    public void SetHotbarActiveItem(InputAction.CallbackContext context) {
        if(context.performed) {
            int hotbarSlot = 1;
            int.TryParse(context.control.displayName, out hotbarSlot);
            playerInteractionController.setHotbarActiveSlot(hotbarSlot - 1);
        }
    }
    #endregion
    #region Input Callbacks - HUD
    public void toggleInventoryScreen(InputAction.CallbackContext context) {
        if(context.canceled) {
            if(playerHudManager.activeHud == PlayerHudManager.HUD.INVENTORY) {
                playerHudManager.changeToHud(PlayerHudManager.HUD.MAIN);
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                playerHudManager.changeToHud(PlayerHudManager.HUD.INVENTORY);
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }
    #endregion
}
