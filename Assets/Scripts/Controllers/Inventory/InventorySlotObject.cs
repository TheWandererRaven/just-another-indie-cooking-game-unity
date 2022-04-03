using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotObject
{
    #region Information
    public string Name = "";
    public string DisplayName = "";
    public string PrefabName = "";
    public short Count = 0;
    public short MaxCount = 0;
    #endregion
    
    #region Constructors
    public InventorySlotObject() {
        this.emptySlot();
    }
    #endregion

    #region Slot Information
    public bool isEmpty() {
        return PrefabName == "";
    }
    #endregion
    
    #region Slot Manipulation - Add
    public void emptySlot() {
        addNewItem("", 0, 0);
    }
    public void addNewItem(string Name, short Count) {
        this.addNewItem(Name, Name, Count);
    }
    public void addNewItem(string Name, string PrefabName, short Count, short MaxCount=99) {
        // Set Display Name to be the Prefab Name
        this.addNewItem(Name, PrefabName, PrefabName, Count, MaxCount:MaxCount);
    }
    public void addNewItem(string Name, short Count, short MaxCount) {
        // Set both the Display, Prefab and Item Name to all be the same
        this.addNewItem(Name, Name, Name, Count, MaxCount);
    }
    public void addNewItem(string Name, string PrefabName, string DisplayName, short Count, short MaxCount=99) {
        this.Name = Name;
        this.PrefabName = PrefabName;
        this.DisplayName = DisplayName;
        this.Count = Count;
        this.MaxCount = MaxCount;
    }
    public short addToStack(short count) {
        int sumResult = this.Count + count;
        int overflowResult = sumResult - MaxCount;
        if(sumResult <= MaxCount) this.Count = (short)sumResult;
        else this.Count = this.MaxCount;
        return ((short)overflowResult);
    }
    #endregion
    
    #region Slot Manipulation - Remove
    public short removeFromStack(short count) {
        int resResult = this.Count - count;
        if(resResult < 0) this.Count = 0;
        else this.Count = (short)resResult;
        return (short)(resResult * -1);
    }
    #endregion
}
