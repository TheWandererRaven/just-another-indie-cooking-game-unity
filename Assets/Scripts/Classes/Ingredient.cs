using UnityEngine;

public class Ingredient
{
    public string Name { get; set; }
    private byte _satiateValue;
    public byte satiateValue { 
        get{
            return _satiateValue;
        } 
        set{
            if(_satiateValue < 0){
                _satiateValue = 0;
            }else{
                _satiateValue = (value > (byte)100) ? (byte)100 : value;
            }
        }
    }
    public float Cost { get; set; }
    public GameObject objectOnTop { get; set; }
    public GameObject objectoOnBot { get; set; }
}