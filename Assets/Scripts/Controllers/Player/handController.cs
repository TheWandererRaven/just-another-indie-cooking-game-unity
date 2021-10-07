using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject equippedItem;
    private RigidbodyConstraints equippedOriginalRBContraints;
    public void equipItem(GameObject item = null) {
        equippedItem = item;
        equippedItem.transform.parent = this.transform;
        equippedItem.transform.position = this.transform.position;
        equippedItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        equippedItem.GetComponent<Collider>().isTrigger = true;
        Rigidbody equippedRB;
        if(equippedItem.TryGetComponent<Rigidbody>(out equippedRB)) {
            equippedOriginalRBContraints = equippedRB.constraints;
            equippedRB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
