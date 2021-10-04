using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float verticalRotationLimitMin = -75f;
    public float verticalRotationLimitMax = 75f;
    public float interactionDistanceMax = 2f;
    public RaycastHit rayHit;
    public GameObject grabbedObject;
    public float grabbedDistance = 2.5f;
    public float addGrabDragSlow = 15f;
    public float grabbedDistanceThreshhold = 2f;
    public string[] raycastLayers = {
        "Interactable"
    };
    private Rigidbody grabbedRigidBody;
    private RigidbodyConstraints grabbedOriginalContraints;
    private float verticalRotation = 0f;
    private LayerMask raycastLayerMask {
        get {
            return LayerMask.GetMask(raycastLayers);
        }
    }
    void Update() {
        // Only need to keep the first "if" to work properly
        if(Physics.Raycast(this.transform.position, this.transform.forward, out rayHit, interactionDistanceMax, raycastLayerMask))
            print(rayHit.transform.gameObject.name);

        if(grabbedObject != null)
            // HANDLE COMMON OBJECT with rigidbody
            if(grabbedRigidBody != null){
                Vector3 forwardPoint = this.transform.position + (this.transform.forward * (grabbedDistance - 1f));
                if(Vector3.Distance(forwardPoint, grabbedObject.transform.position) <= grabbedDistanceThreshhold){
                    Vector3 vectorDistance = forwardPoint - grabbedObject.transform.position;
                    grabbedRigidBody.AddForce(
                        vectorDistance * 100f
                    );
                } else dropGrabbedObject();
            } else
                // HANDLE UNCOMON OBJECT without rigidbody
                grabbedObject.transform.position = this.transform.position + (this.transform.forward * (grabbedDistance - 1f));
    }
    public void rotateVertically(float addRotation) {
        float newRotation = verticalRotation + addRotation;
        if(newRotation <= verticalRotationLimitMax && newRotation >= verticalRotationLimitMin){
            verticalRotation = newRotation;
            this.transform.Rotate(new Vector3(addRotation, 0, 0), Space.Self);
        }
    }
    public void crouch(float newHeight) {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, newHeight, this.transform.localPosition.z);
    }
    public bool hasGrabbableInSights() { 
        return rayHit.transform != null ? rayHit.transform.CompareTag("Grabbable") : false;
    }
    public bool hasInteractableInSights() { 
        return rayHit.transform != null ? rayHit.transform.CompareTag("Interactable") : false;
    }
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
}
