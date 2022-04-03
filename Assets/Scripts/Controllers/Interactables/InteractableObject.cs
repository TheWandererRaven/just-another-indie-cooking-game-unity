using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string Name = "";
    public string DisplayName = "";
    public abstract void interact(GameObject handObject=null);
}
