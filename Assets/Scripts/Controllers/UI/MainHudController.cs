using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHudController : MonoBehaviour
{
    #region Controller Configuration
    public TextMeshProUGUI mainPlayerMessageText;
    public HotbarUIController hotbarUIController;
    #endregion
    
    #region UI Actions
    public void setMainPlayerMessageText(InteractionController.INTERACTION_TYPE interactionType, InteractableObject pickableObject) {
        if(pickableObject.DisplayName != "")
            mainPlayerMessageText.text = $"{getInteractionText(interactionType)} {pickableObject.DisplayName}";
    }
    public void setMainPlayerMessageText(string message) {
        mainPlayerMessageText.text = message;
    }
    #endregion
    
    #region Auxiliary Methods
    private static string getInteractionText(InteractionController.INTERACTION_TYPE interactionType) {
        switch (interactionType) {
            case(InteractionController.INTERACTION_TYPE.PICKUP): return "Pick up";
            default: return "Interact with";
        }
        
    }
    #endregion
}
