using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotObject
{
    public string Name = "";
    public string DisplayName = "";
    public short Count = 0;
    public short MaxCount = 0;
    public InventorySlotObject() {
        this.emptySlot();
    }
    public void emptySlot() {
        addNewItem("", 0, 0);
    }
    public void addNewItem(string Name, short Count) {
        this.addNewItem(Name, Name, Count);
    }
    public void addNewItem(string Name, string DisplayName, short Count) {
        this.addNewItem(Name, DisplayName, Count);
    }
    public void addNewItem(string Name, short Count, short MaxCount) {
        this.addNewItem(Name, Name, Count, MaxCount);
    }
    public void addNewItem(string Name, string DisplayName, short Count, short MaxCount=99) {
        this.Name = Name;
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
    public short removeFromStack(short count) {
        int resResult = this.Count - count;
        if(resResult < 0) this.Count = 0;
        else this.Count = (short)resResult;
        return (short)(resResult * -1);
    }
}
