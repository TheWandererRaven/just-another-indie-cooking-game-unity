using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float verticalRotationLimitMin = -75f;
    public float verticalRotationLimitMax = 75f;
    private float verticalRotation = 0f;
    public void rotateVertically(float addRotation) {
        float newRotation = verticalRotation + addRotation;
        if(newRotation <= verticalRotationLimitMax && newRotation >= verticalRotationLimitMin){
            verticalRotation = newRotation;
            this.transform.Rotate(new Vector3(addRotation, 0, 0), Space.Self);
        }
    }
}
