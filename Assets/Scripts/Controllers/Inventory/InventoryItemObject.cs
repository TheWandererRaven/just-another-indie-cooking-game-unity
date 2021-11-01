using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemObject
{
    public GameObject WorldObject;
    public string Name;
    public short Count = 1;
    public short MaxCount = 999;
    public short addToStack(short count) {
        int sumResult = this.Count + count;
        int overflowResult = sumResult - MaxCount;
        if(overflowResult < 0) overflowResult = 0;
        return ((short)overflowResult);
    }
}
