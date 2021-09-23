using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeedUnitsPerSecond = 2.5f;
    private float movementSpeedBase {
        get {
            return movementSpeedUnitsPerSecond * Time.deltaTime;
        }
    }
    private Vector3 calculateAxisPosition(Vector3 Axis, float direction) {
        return direction * movementSpeedBase * Axis;
    }
    public void moveDirectionally(float horizontalMovement, float verticalMovement) {
        this.transform.position = this.transform.position + 
        calculateAxisPosition(this.transform.forward, verticalMovement) +
        calculateAxisPosition(this.transform.right, horizontalMovement);
    }
    public void moveForcefully(float horizontalMovement, float verticalMovement) {
        Vector3 force = new Vector3(horizontalMovement, 0, verticalMovement);
        print(force);
        this.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
    public void rotateDirectionally(float horizontalRotation, float verticalRotation) {
        this.transform.Rotate(new Vector3(verticalRotation, horizontalRotation, 0), Space.Self);
    }
}
