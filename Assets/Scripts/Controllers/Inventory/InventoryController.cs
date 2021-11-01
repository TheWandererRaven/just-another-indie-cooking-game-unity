using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public byte invetorySize = 5;
    public InventoryItemObject[] Items;
    void Start() {
        Items = new InventoryItemObject[invetorySize];
    }
    void Update() {
        
    }
    public InventoryItemObject addToStorage(InventoryItemObject item) {
        for(int i = 0; i < invetorySize; i++) {
            if(Items[i] == null) {
                Items[i] = item;
                break;
            } else if(Items[i].Name.Equals(item.Name)) {
                short leftoverCount = Items[i].addToStack(item.Count);
                if(leftoverCount > 0) {
                    item.Count = leftoverCount;
                    return item;
                } else break;
            }
        }
        return null;
    }
    public InventoryItemObject removeFromStorage(int position, int amount) {
        InventoryItemObject removedItem = null;
        if(position < this.invetorySize) if(Items[position] != null) {
            removedItem = Items[position];
            Items[position] = null;
        }
        return removedItem;
    }
    public void moveStoragePosition(int prevPosition, int newPosition) {
        if(prevPosition < this.invetorySize && newPosition < this.invetorySize) if(Items[prevPosition] != null) {
            InventoryItemObject itemToMove = Items[prevPosition];
            InventoryItemObject itemToReplace = null;
            if(Items[newPosition] != null) itemToReplace = Items[newPosition];
            Items[newPosition] = itemToMove;
            Items[prevPosition] = itemToReplace;
        }
    }
}
