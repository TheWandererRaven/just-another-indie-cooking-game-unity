using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionController : MonoBehaviour
{
    #region Controller Configuration
    public Transform playerCamera;
    public PlayerInput playerInput;
    public PlayerHudManager playerHudManager;
    //public TextMeshProUGUI playerInteractionText;
    //public HotbarUIController hotbarUIController;
    public HandController handController;
    public InventoryController inventoryController;
    public float interactionDistanceMax = 2f;
    public float grabbedDistance = 2.5f;
    public float addGrabDragSlow = 15f;
    public float grabbedDistanceThreshhold = 2f;
    public float throwForce = 5f;
    public string[] raycastLayers = {
        "Interactable"
    };
    #endregion

    #region Script only variables
    public GameObject grabbedObject;
    private RaycastHit rayHit;
    private Rigidbody grabbedRigidBody;
    private RigidbodyConstraints grabbedOriginalContraints;
    private LayerMask raycastLayerMask {
        get {
            return LayerMask.GetMask(raycastLayers);
        }
    }
    #endregion
    
    #region GameObject Events
    void Update() {
        // Only need to keep the first "if" to work properly
        if(Physics.Raycast(playerCamera.position, playerCamera.forward, out rayHit, interactionDistanceMax, raycastLayerMask) && !hasGrabbedObject())
            playerHudManager.mainHudController.setMainPlayerMessageText(hasPickableInSights() ? INTERACTION_TYPE.PICKUP : INTERACTION_TYPE.INTERACTION, rayHit.transform.GetComponent<InteractableObject>());
        else
            playerHudManager.mainHudController.setMainPlayerMessageText("");

        moveGrabbedObject();
    }
    #endregion
    
    #region Validation Methods
    public bool hasGrabbableInSights() {
        return rayHit.transform != null ? rayHit.transform.CompareTag("Grabbable") : false;
    }
    public bool hasInteractableInSights() {
        return rayHit.transform != null ? rayHit.transform.CompareTag("Interactable") : false;
    }
    public bool hasPickableInSights() {
        return rayHit.transform != null ? rayHit.transform.CompareTag("Pickable") : false;
    }
    public bool hasActionableInSights() {
        return rayHit.transform != null ? rayHit.transform.CompareTag("Actionable") : false;
    }
    public bool hasGrabbedObject() {
        return grabbedObject != null;
    }
    #endregion

    #region Auxiliary Methods
    public GameObject getGameObjectInSights() {
        return rayHit.transform.gameObject;
    }
    private void moveGrabbedObject() {
        if(grabbedObject != null)
            // HANDLE OBJECT with rigidbody
            if(grabbedRigidBody != null){
                Vector3 forwardPoint = playerCamera.position + (playerCamera.forward * (grabbedDistance - 1f));
                if(Vector3.Distance(forwardPoint, grabbedObject.transform.position) <= grabbedDistanceThreshhold){
                    Vector3 vectorDistance = forwardPoint - grabbedObject.transform.position;
                    grabbedRigidBody.AddForce(
                        vectorDistance * 100f
                    );
                } else releaseGrabbedObject();
            } else
                // HANDLE OBJECT without rigidbody
                grabbedObject.transform.position = playerCamera.position + (playerCamera.forward * (grabbedDistance - 1f));
    }
    #endregion

    #region Action Methods
    public void grabObjectInSights() {
        if(hasGrabbableInSights() || hasPickableInSights() || hasActionableInSights()){
            grabbedObject = getGameObjectInSights();
            if(grabbedObject.TryGetComponent<Rigidbody>(out grabbedRigidBody)){
                grabbedRigidBody.drag += addGrabDragSlow;
                grabbedRigidBody.useGravity = false;
                grabbedOriginalContraints = grabbedRigidBody.constraints;
                grabbedRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
    public void releaseGrabbedObject() {
        if(grabbedObject != null) {
            if(grabbedRigidBody != null){
                grabbedRigidBody.useGravity = true;
                grabbedRigidBody.constraints = grabbedOriginalContraints;
                grabbedRigidBody.drag -= addGrabDragSlow;
            }
            grabbedRigidBody = null;
            grabbedObject = null;
        }
    }
    public void throwGrabbedObject() {
        GameObject throwableObject = grabbedObject;
        releaseGrabbedObject();
        Rigidbody throwableRB = null;
        if(throwableObject.TryGetComponent<Rigidbody>(out throwableRB)) throwableRB.AddForce(
            throwForce * playerCamera.forward, ForceMode.Impulse
        );
    }
    public void interactWithObjectInSights() {
        if(!hasGrabbedObject())
            if(hasPickableInSights() || hasActionableInSights()) pickUp(getGameObjectInSights());
            else if(hasInteractableInSights()) getGameObjectInSights().GetComponent<InteractableObject>().interact();
    }
    public void equipOnHand(GameObject item) {
        handController.equipItem(item);
    }
    public void pickUp(GameObject item) {
        PickableObjectController pickableController = null;
        if(item.TryGetComponent<PickableObjectController>(out pickableController)) {
            pickableController.changeStackCount(inventoryController.addToStorage(item));
            playerHudManager.RefreshHotbar();
        }
        //TODO: addToStorage(GameObject) automatically find the correct slot and add it to it, if full, add to another slot, if no slots available, don't do anything
        // will have to disable equipability for the moment
    }
    public void executePrimaryAction(InputActionPhase phase) {
        if(grabbedObject != null) this.throwGrabbedObject();
        else handController.executePrimaryAction(phase);
    }
    public void executeSecondaryAction(InputActionPhase phase) {
        handController.executeSecondaryAction(phase);
    }
    #endregion

    public enum INTERACTION_TYPE
    {
        INTERACTION = 0,
        PICKUP = 1
    }
}
