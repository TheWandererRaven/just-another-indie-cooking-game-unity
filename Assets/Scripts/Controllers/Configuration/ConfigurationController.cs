using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationController : MonoBehaviour
{
    private KeyCode _walkForwardKey;
    private KeyCode _walkBackwardKey;
    private KeyCode _walkLeftKey;
    private KeyCode _walkRightKey;
    private float _mouseSensitivity;
    public float mouseSensitivityMax = 50f;
    public float mouseSensitivityMin = 10f;
    public KeyCode walkForwardKey {
        get { return _walkForwardKey; }
        set { _walkForwardKey = value; }
    }
    public KeyCode walkBackwardKey {
        get { return _walkBackwardKey; }
        set { _walkBackwardKey = value; }
    }
    public KeyCode walkLeftKey {
        get { return _walkLeftKey; }
        set { _walkLeftKey = value; }
    }
    public KeyCode walkRightKey {
        get { return _walkRightKey; }
        set { _walkRightKey = value; }
    }
    public float mouseSensitivity {
        get { return _mouseSensitivity; }
        set { _mouseSensitivity = 
                value < mouseSensitivityMin ? mouseSensitivityMin : 
                value > mouseSensitivityMax ? mouseSensitivityMax :
                value;
            }
    }
    public float lookSpeedMultiplier {
        get { return ((mouseSensitivityMax + mouseSensitivityMin)/2) / mouseSensitivity; }
    }
    ConfigurationController() {
        // DEFAULT CONFIG
        _walkForwardKey = KeyCode.W;
        _walkLeftKey = KeyCode.A;
        _walkBackwardKey = KeyCode.S;
        _walkRightKey = KeyCode.D;
        _mouseSensitivity = 30f;
    }
}
