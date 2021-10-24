using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject equippedItem;
    public float dropDistanceForward = 0.5f;
    private RigidbodyConstraints equippedOriginalRBContraints;
    public bool isEquippedObjectActionable() {
        return equippedItem != null ? equippedItem.CompareTag("Actionable") : false;
    }
    public void equipItem(GameObject item = null) {
        if(item != null){
            if(equippedItem != null) this.dropEquipped();
            equippedItem = item;
            equippedItem.transform.parent = this.transform;
            equippedItem.transform.position = this.transform.position;
            EquippableObject equippableController;
            if(equippedItem.TryGetComponent<EquippableObject>(out equippableController)) equippedItem.transform.localRotation = Quaternion.Euler(equippableController.equippedRotation);
            else equippedItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Rigidbody equippedRB;
            if(equippedItem.TryGetComponent<Rigidbody>(out equippedRB)) {
                equippedOriginalRBContraints = equippedRB.constraints;
                equippedRB.constraints = RigidbodyConstraints.FreezeAll;
            }
            // TODO: ADD ITEM TO INVENTORY
        }
    }
    public void dropEquipped() {
        // TODO: REMOVE ITEM FROM INVENTORY
        equippedItem.transform.parent = null;
        equippedItem.transform.position += equippedItem.transform.forward * dropDistanceForward;
        Rigidbody equippedRB;
        if(equippedItem.TryGetComponent<Rigidbody>(out equippedRB)) equippedRB.constraints = equippedOriginalRBContraints;
        equippedItem = null;
    }
}
