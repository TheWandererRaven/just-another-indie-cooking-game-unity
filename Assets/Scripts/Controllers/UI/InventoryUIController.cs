using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public GameObject InventorySlotDisplayPrefab = null;
    public GameObject InventoryContainerDisplay = null;
    public InventoryController playerInventory = null;
    public ItemsCatalogController itemsCatalog = null;
    public Color SlotColor = Color.white;
    public Color HotbarColor = Color.white;
    private List<GameObject> inventorySlots = new List<GameObject>();
    // Start is called before the first frame update
    void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
    }
    void OnEnable()
    {
        if(inventorySlots.Count <= 0) GenerateInventoryUIDisplay();
        LoadPlayerInventory();
    }
    void GenerateInventoryUIDisplay() {
        (int colNum, int rowNum) = CalculationsHelper.CalculateInventoryColRowNumSize(playerInventory.inventorySize);
        Rect InventoryContainerDisplayRect = InventoryContainerDisplay.GetComponent<RectTransform>().rect;
        float slotWidth = InventoryContainerDisplayRect.width / (colNum + 1);
        float slotHeight = InventoryContainerDisplayRect.height / (rowNum + 1);
        float horizontalSpacing = slotWidth / (colNum + 1);
        float verticalSpacing = slotHeight / (rowNum + 1);
        int cellNum = 0;
        for(int i = 0; i < rowNum; i++)
            for(int j = 0; j < colNum && cellNum < playerInventory.inventorySize; j++) {
                GameObject newSlot = Instantiate(InventorySlotDisplayPrefab, InventoryContainerDisplay.transform);
                RectTransform newSlotRect = newSlot.GetComponent<RectTransform>();
                Vector2 xy1 = new Vector2(
                    (horizontalSpacing * (j + 1)) + (slotWidth * j),
                    (verticalSpacing * (i + 1)) + (slotHeight * i)
                );
                Vector2 xy2 = new Vector2(
                    xy1.x + slotWidth,
                    xy1.y + slotHeight
                );
                newSlotRect.anchorMin = new Vector2(
                    xy1.x / InventoryContainerDisplayRect.width,
                    1 - (xy2.y / InventoryContainerDisplayRect.height)
                );
                newSlotRect.anchorMax = new Vector2(
                    xy2.x / InventoryContainerDisplayRect.width,
                    1 - (xy1.y / InventoryContainerDisplayRect.height)
                );
                Image SlotBaseImage = newSlot.GetComponent<Image>();
                if(cellNum < playerInventory.hotbarSize) SlotBaseImage.color = HotbarColor;
                else SlotBaseImage.color = SlotColor;
                cellNum += 1;
                inventorySlots.Add(newSlot);
            }
    }
    public void LoadPlayerInventory() {
        // Read with for loop each slot in player inventory and for each i position, in the UI slots, retrieve the sprite from the catalog and then generate sprite object on slot display
        for(int i = 0; i < playerInventory.inventorySize; i++) {
                RawImage icon = inventorySlots[i].GetComponentInChildren<RawImage>();
                TextMeshProUGUI itemCountText = inventorySlots[i].GetComponentInChildren<TextMeshProUGUI>();
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
