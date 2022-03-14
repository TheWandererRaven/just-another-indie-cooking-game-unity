using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCatalogController : MonoBehaviour
{
    public GameObject[] itemsCatalog;
    public Sprite[] imagesCatalog;
    public GameObject defaultItem;
    public Sprite defaultSprite;
    private Dictionary<string, GameObject> _itemsCatalog = new Dictionary<string, GameObject>();
    void Start() {
        foreach(GameObject item in itemsCatalog) _itemsCatalog.Add(item.name, item);
    }
    public GameObject getPrefab(string name) {
        GameObject returnValue = defaultItem;
        _itemsCatalog.TryGetValue(name, out returnValue);
        return returnValue;
    }
    public Sprite getItemSprite(string name) {
        Sprite returnSprite = defaultSprite;
        foreach(Sprite sprt in imagesCatalog) if(sprt.name.Equals(name)) returnSprite = sprt;
        return returnSprite;
    }
}
