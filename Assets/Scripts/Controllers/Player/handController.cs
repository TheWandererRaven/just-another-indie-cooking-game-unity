using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject equippedItem;
    private RigidbodyConstraints equippedOriginalRBContraints;
    public bool isEquippedObjectActionable() {
        return equippedItem != null ? equippedItem.CompareTag("Actionable") : false;
    }
    public void equipItem(GameObject item = null) {
        equippedItem = item;
        if(equippedItem != null){
            equippedItem.transform.parent = this.transform;
            equippedItem.transform.position = this.transform.position;
            EquippableObject equippableController;
            if(equippedItem.TryGetComponent<EquippableObject>(out equippableController)) equippedItem.transform.localRotation = Quaternion.Euler(equippableController.equippedRotation);
            else equippedItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //equippedItem.GetComponent<Collider>().isTrigger = true;
            Rigidbody equippedRB;
            if(equippedItem.TryGetComponent<Rigidbody>(out equippedRB)) {
                equippedOriginalRBContraints = equippedRB.constraints;
                equippedRB.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
