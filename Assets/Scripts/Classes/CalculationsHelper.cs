using UnityEngine;

public static class CalculationsHelper {
    public static (int, int) CalculateInventoryColRowNumSize(short inventorySize) {
        int colNum = Mathf.CeilToInt(Mathf.Sqrt(inventorySize));
        return (colNum, Mathf.CeilToInt(((float)inventorySize)/colNum));
    }
}