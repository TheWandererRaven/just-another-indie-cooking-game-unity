using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUIController : MonoBehaviour
{
    public GameObject HotbarSlotDisplayPrefab = null;
    public GameObject HotbarContainerDisplay = null;
    public InventoryController playerInventory = null;
    public ItemsCatalogController itemsCatalog = null;
    private List<GameObject> hotbarSlots = new List<GameObject>();
    private byte activeSlot = 0;
    // Start is called before the first frame update
    void Start()
    {
        GenerateHotbarUIDisplay();
    }
    void OnEnable()
    {
        if(hotbarSlots.Count > 0) RefreshItemsIcons();
    }
    void GenerateHotbarUIDisplay() {
        Rect InventoryContainerDisplayRect = HotbarContainerDisplay.GetComponent<RectTransform>().rect;
        float slotSide = (InventoryContainerDisplayRect.height * 0.8f) / InventoryContainerDisplayRect.width;
        float horizontalSpacing = (1 - (slotSide * playerInventory.hotbarSize)) / (playerInventory.hotbarSize + 1);
        for(int i = 0; i < playerInventory.hotbarSize; i++) {
            GameObject newSlot = Instantiate(HotbarSlotDisplayPrefab, HotbarContainerDisplay.transform);
            RectTransform newSlotRect = newSlot.GetComponent<RectTransform>();
            newSlotRect.anchorMin = new Vector2(
                (horizontalSpacing * (i + 1)) + (slotSide * i),
                0.1f
            );
            newSlotRect.anchorMax = new Vector2(
                newSlotRect.anchorMin.x + slotSide,
                0.9f
            );
            ItemSlotUIController slotController;
            if(newSlot.TryGetComponent<ItemSlotUIController>(out slotController))
                slotController.initializeSlot(i + 1);
            hotbarSlots.Add(newSlot);
        }
    }
    public void RefreshItemsIcons() {
        for(int i = 0; i < playerInventory.hotbarSize; i++) {
            ItemSlotUIController slotController;
            if(hotbarSlots[i].TryGetComponent<ItemSlotUIController>(out slotController))
                if(!playerInventory.Slots[i].Name.Equals("")) {
                    slotController.UpdateData(
                        playerInventory.Slots[i].Count,
                        itemsCatalog.getItemSprite(playerInventory.Slots[i].Name),
                        Color.white
                    );
                    //icon.texture = itemsCatalog.getItemSprite(playerInventory.Slots[i].Name);
                    //icon.color = Color.white;
                    //itemCountText.text = playerInventory.Slots[i].Count.ToString();
                } else {
                    slotController.UpdateData(
                        0,
                        null,
                        new Color(1, 1, 1, 0)
                    );
                    //icon.texture = null;
                    //icon.color = new Color(1, 1, 1, 0);
                    //itemCountText.text = "";
            }
        }
    }
    public void HighlightActiveSlot(int slotToHighlight) {
        print($"Activates slot number: {slotToHighlight}");
        hotbarSlots[activeSlot].GetComponent<ItemSlotUIController>().DeactivateSlot();
        hotbarSlots[slotToHighlight].GetComponent<ItemSlotUIController>().ActivateSlot();
        activeSlot = (byte)slotToHighlight;
        print($"Active slot: {activeSlot}");
    }
}
