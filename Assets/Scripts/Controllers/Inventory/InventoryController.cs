using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public byte invetorySize = 5;
    public InventorySlotObject[] Slots;
    void Start() {
        GenerateInventory();
    }
    void Update() {
        string printableInv = "";
        foreach(InventorySlotObject slot in Slots) printableInv += $" [{slot.DisplayName}: {slot.Count}/{slot.MaxCount}] ";
        print(printableInv);
    }
    public void GenerateInventory() {
        Slots = new InventorySlotObject[invetorySize];
        for(int i = 0; i < Slots.Length; i++) {
            Slots[i] = new InventorySlotObject();
        }
    }
    public short addToStorage(GameObject item) {
        // item must have PickableOjbect controller
        short leftoverCount = 0;
        PickableObjectController pickableController = null;
        if(item.TryGetComponent<PickableObjectController>(out pickableController)){
            leftoverCount = pickableController.Count;
            for(int i = 0; i < invetorySize; i++) {
                 if(Slots[i].Name.Equals(pickableController.Name) && Slots[i].Count < Slots[i].MaxCount) {
                    leftoverCount = Slots[i].addToStack(pickableController.Count);
                    break;
                } else if(Slots[i].Name.Equals("")) {
                    Slots[i].addNewItem(pickableController.Name, item.name, pickableController.DisplayName, pickableController.Count, pickableController.MaxCount);
                    leftoverCount = 0;
                    break;
                } 
            }
        }
        return leftoverCount;
    }
    public short addToStorage(string Name, short Count, short MaxStorage = 999) {
        return addToStorage(Name, Name, Name, Count, MaxStorage);
    }
    public short addToStorage(string Name, string PrefabName, short Count, short MaxStorage = 999) {
        return addToStorage(Name, PrefabName, PrefabName, Count, MaxStorage);
    }
    public short addToStorage(string Name, string PrefabName, string DisplayName, short Count, short MaxStorage = 999) {
        short leftoverCount = Count;
        for(int i = 0; i < invetorySize; i++) {
            if(Slots[i].Name.Equals(Name) && Slots[i].Count < Slots[i].MaxCount) {
                leftoverCount = Slots[i].addToStack(Count);
                break;
            } else if(Slots[i].Name.Equals("")) {
                Slots[i].addNewItem(Name, PrefabName, DisplayName, Count, MaxStorage);
                leftoverCount = 0;
                break;
            } 
        }
        return leftoverCount;
    }
    public string removeFromStorage(int position, short amount, out short removedAmount) {
        string removedItemName = "";
        removedAmount = 0;
        if(position < this.invetorySize) if(Slots[position] != null) {
            removedItemName = Slots[position].Name;
            if(Slots[position].Count <= amount) {
                removedAmount = Slots[position].Count;
                Slots[position].emptySlot();
            } else {
                removedAmount = amount;
                Slots[position].removeFromStack(amount);
            }
        }
        return removedItemName;
    }
    public void moveStoragePosition(int prevPosition, int newPosition) {
        if(prevPosition < this.invetorySize && newPosition < this.invetorySize) if(Slots[prevPosition] != null) {
            InventorySlotObject itemToMove = Slots[prevPosition];
            InventorySlotObject itemToReplace = Slots[newPosition];
            Slots[newPosition] = itemToMove;
            Slots[prevPosition] = itemToReplace;
        }
    }
}
