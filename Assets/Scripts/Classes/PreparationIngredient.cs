using UnityEngine;

public class PreparationIngredient
{
    public int preparationTime { get; set; }
    private byte _currentPreparation;
    public byte currentPreparation { 
        get{
            return _currentPreparation;
        }
        set{
            if(value < 0){
                _currentPreparation = 0;
            }else{
                _currentPreparation = (value > (byte)100) ? (byte)100 : value;
            }
        }
    }
    public GameObject rawPrefab { get; set; }
    public GameObject preparedPrefab { get; set; }
    public GameObject overcookedPrefab { get; set; }
}