using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectController : InteractableObject
{
    public short Count = 0;
    public short MaxCount = 0;
    public void changeStackCount(short count) {
        if(count <= 0) Destroy(this.gameObject, 0.0f);
        else this.Count = count;
    }
    public override void interact(GameObject handObject=null)
    {
        InteractionController interactionController;
        // Pick Up
        if(handObject == null) print("Need Hand object");
        // TODO: replace script to add pickable to inventory
        //else if(handObject.TryGetComponent<InteractionController>(out interactionController)) interactionController.equipOnHand(this.gameObject);
    }
}
