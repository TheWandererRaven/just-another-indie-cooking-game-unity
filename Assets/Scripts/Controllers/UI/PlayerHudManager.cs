using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudManager : MonoBehaviour
{
    #region Controller Configuration
    public MainHudController mainHudController;
    public HUD activeHud = HUD.MAIN;
    #endregion
    
    #region UI Actions
    public void changeToHud(HUD hud) {
        this.transform.GetChild((int)hud).gameObject.SetActive(true);
        this.transform.GetChild((int)activeHud).gameObject.SetActive(false);
        activeHud = hud;
    }
    public void RefreshHotbar() {
        mainHudController.hotbarUIController.RefreshItemsIcons();
    }
    public void HighlightActiveSlot(int slotIndex) {
        mainHudController.hotbarUIController.HighlightActiveSlot(slotIndex);
    }
    #endregion
    
    #region UI Information
    public bool IsHudActive(HUD hud) {
        return this.transform.GetChild((int)hud).gameObject.activeSelf;
    }
    #endregion
    public enum HUD
    {
        MAIN = 0,
        INVENTORY = 1
    }
}
