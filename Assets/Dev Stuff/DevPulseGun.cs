using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPulseGun : EquippableObject, PrimaryActionableObject
{
    public Collider pulseCollider;
    public float pulseForce = 10f;
    public float pulseRadius = 2f;
    private bool isRepelling = false;
    public void primaryAction_Single()
    {
        isRepelling = true;
    }
    public void primaryAction_Hold()
    {

    }
    public void primaryAction_Canceled()
    {
        isRepelling = false;
    }
    void OnTriggerStay(Collider collision){
        if(isRepelling){
            Rigidbody colRB;
            if(collision.TryGetComponent<Rigidbody>(out colRB)) colRB.AddExplosionForce(pulseForce, this.transform.position, pulseRadius, 0, ForceMode.Force);
        }
    }
}
