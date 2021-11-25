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
    // Start is called before the first frame update
    void Start()
    {
        GenerateHotbarUIDisplay();
    }
    void OnEnable()
    {
        //if(hotbarSlots.Count <= 0) GenerateHotbarUIDisplay();
        RefreshItemsIcons();
    }
    void GenerateHotbarUIDisplay() {
        Rect InventoryContainerDisplayRect = HotbarContainerDisplay.GetComponent<RectTransform>().rect;
        float slotSide = (InventoryContainerDisplayRect.height * 0.8f) / InventoryContainerDisplayRect.width;
        float horizontalSpacing = (1 - (slotSide * playerInventory.hotbarSize)) / (playerInventory.hotbarSize + 1);
        print(slotSide);
        print(horizontalSpacing);
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
            hotbarSlots.Add(newSlot);
        }
    }
    public void RefreshItemsIcons() {
        for(int i = 0; i < playerInventory.hotbarSize; i++) {
                RawImage icon = hotbarSlots[i].GetComponentInChildren<RawImage>();
                TextMeshProUGUI itemCountText = hotbarSlots[i].GetComponentInChildren<TextMeshProUGUI>();
            if(!playerInventory.Slots[i].Name.Equals("")) {
                icon.texture = itemsCatalog.getItemSprite(playerInventory.Slots[i].Name);
                icon.color = Color.white;
                itemCountText.text = playerInventory.Slots[i].Count.ToString();
            } else {
                icon.texture = null;
                icon.color = new Color(1, 1, 1, 0);
                itemCountText.text = "";
            }
        }
    }
}
