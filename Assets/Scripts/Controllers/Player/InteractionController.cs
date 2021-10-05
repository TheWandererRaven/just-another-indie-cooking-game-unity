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
    public float interactionDistanceMax = 2f;
    public float grabbedDistance = 2.5f;
    public float addGrabDragSlow = 15f;
    public float grabbedDistanceThreshhold = 2f;
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
            string interactControl = playerInput.actions.FindAction("Interact3").GetBindingDisplayString();
            string interactionType = hasPickupableInSights() ? "pick up" : "interact with";
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
    public bool hasPickupableInSights() {
        return rayHit.transform != null ? rayHit.transform.CompareTag("Pickupable") : false;
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
        if(hasGrabbableInSights()){
            grabbedObject = getGameObjectInSights();
            //grabbedObject.transform.SetParent(this.transform);
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
            //grabbedObject.transform.SetParent(null);
            grabbedRigidBody = null;
            grabbedObject = null;
        }
    }
    #endregion
}
