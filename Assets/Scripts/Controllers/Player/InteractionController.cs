using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionController : MonoBehaviour
{
    #region Controller Configuration
    public Transform PlayerCamera;
    public PlayerInput playerInput;
    public TextMeshProUGUI playerInteractionText;
    public HandController handController;
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
    void Update() {
        // Only need to keep the first "if" to work properly
        if(Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out rayHit, interactionDistanceMax, raycastLayerMask) && !hasGrabbedObject()){
            string objectName = rayHit.transform.name;
            string interactControl = playerInput.actions.FindAction("Interact").GetBindingDisplayString();
            string interactionType = hasPickableInSights() ? "pick up" : "interact with";
            playerInteractionText.text = hasGrabbableInSights() ? "" : $"Press {interactControl} to {interactionType} {objectName}";
        } else playerInteractionText.text = "";

        if(grabbedObject != null)
            // HANDLE COMMON OBJECT with rigidbody
            if(grabbedRigidBody != null){
                Vector3 forwardPoint = PlayerCamera.position + (PlayerCamera.forward * (grabbedDistance - 1f));
                if(Vector3.Distance(forwardPoint, grabbedObject.transform.position) <= grabbedDistanceThreshhold){
                    Vector3 vectorDistance = forwardPoint - grabbedObject.transform.position;
                    grabbedRigidBody.AddForce(
                        vectorDistance * 100f
                    );
                } else dropGrabbedObject();
            } else
                // HANDLE UNCOMON OBJECT without rigidbody
                grabbedObject.transform.position = PlayerCamera.position + (PlayerCamera.forward * (grabbedDistance - 1f));
    }
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

    #region Action Methods
    public GameObject getGameObjectInSights() {
        return rayHit.transform.gameObject;
    }
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
    public void dropGrabbedObject() {
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
        dropGrabbedObject();
        Rigidbody throwableRB = null;
        if(throwableObject.TryGetComponent<Rigidbody>(out throwableRB)) throwableRB.AddForce(
            throwForce * PlayerCamera.forward, ForceMode.Impulse
        );
    }
    public void interactWithObjectInSights() {
        if(!hasGrabbedObject())
            if(hasPickableInSights() || hasActionableInSights()) getGameObjectInSights().GetComponent<PickableObjectController>().interact(this.gameObject);
            else if(hasInteractableInSights()) getGameObjectInSights().GetComponent<InteractableObject>().interact();
    }
    public void equipOnHand(GameObject item) {
        handController.equipItem(item);
    }
    public void executePrimaryAction(InputActionPhase phase) {
        if(grabbedObject != null) this.throwGrabbedObject();
        else if(handController.isEquippedObjectActionable()){
            if(phase == InputActionPhase.Started)
                foreach(PrimaryActionableObject obj in handController.equippedItem.GetComponents<PrimaryActionableObject>()) obj.primaryAction_Single();
            else if(phase == InputActionPhase.Performed)
                foreach(PrimaryActionableObject obj in handController.equippedItem.GetComponents<PrimaryActionableObject>()) obj.primaryAction_Hold();
            else
                foreach(PrimaryActionableObject obj in handController.equippedItem.GetComponents<PrimaryActionableObject>()) obj.primaryAction_Canceled();
        }
    }
    public void executeSecondaryAction(InputActionPhase phase) {
        if(handController.isEquippedObjectActionable()){
            if(phase == InputActionPhase.Started)
                foreach(SecondaryActionableObject obj in handController.equippedItem.GetComponents<SecondaryActionableObject>()) obj.secondaryAction_Single();
            else if(phase == InputActionPhase.Performed)
                foreach(SecondaryActionableObject obj in handController.equippedItem.GetComponents<SecondaryActionableObject>()) obj.secondaryAction_Hold();
            else 
                foreach(SecondaryActionableObject obj in handController.equippedItem.GetComponents<SecondaryActionableObject>()) obj.secondaryAction_Canceled();
        }
    }
    #endregion
}
