using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectController : InteractableObject
{
    public override void interact(GameObject handObject=null)
    {
        // EMPTY HANDED INTERACTION
        if(handObject == null) print("Object is being picked up!");
    }
}
