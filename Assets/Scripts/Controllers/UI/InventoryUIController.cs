using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public GameObject InventorySlotDisplayPrefab = null;
    public GameObject InventoryContainerDisplay = null;
    public InventoryController playerInventory = null;
    // Start is called before the first frame update
    void Start()
    {
        GenerateInventoryUIDisplay();
    }
    void GenerateInventoryUIDisplay() {
        int colNum = Mathf.CeilToInt(Mathf.Sqrt(playerInventory.invetorySize));
        int rowNum = Mathf.CeilToInt(((float)playerInventory.invetorySize)/colNum);
        Rect InventoryContainerDisplayRect = InventoryContainerDisplay.GetComponent<RectTransform>().rect;
        float slotWidth = InventoryContainerDisplayRect.width / (colNum + 1);
        float slotHeight = InventoryContainerDisplayRect.height / (rowNum + 1);
        float horizontalSpacing = slotWidth / (colNum + 1);
        float verticalSpacing = slotHeight / (rowNum + 1);
        int cellNum = 0;
        for(int i = 0; i < rowNum; i++)
            for(int j = 0; j < colNum && cellNum < playerInventory.invetorySize; j++) {
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
                cellNum += 1;
            }
    }
    public void DisplayPlayerInventory() {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
