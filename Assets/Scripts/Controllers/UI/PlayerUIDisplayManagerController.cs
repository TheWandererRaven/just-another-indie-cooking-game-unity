using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIDisplayManagerController : MonoBehaviour
{
    public void changeDisplay(string displayName) {
        for(int i = 0; i < this.transform.childCount; i++){
            GameObject displayGO = this.transform.GetChild(i).gameObject;
            displayGO.SetActive(displayGO.name.Equals(displayName));
        }
    }
    public bool IsDisplayActive(string displayName) {
        bool isActive = false;
        for(int i = 0; i < this.transform.childCount; i++){
            GameObject displayGO = this.transform.GetChild(i).gameObject;
            if(displayGO.name.Equals(displayName)) {
                isActive = displayGO.activeSelf;
                break;
            }
        }
        return isActive;
    }
}
