using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCatalogController : MonoBehaviour
{
    public GameObject[] itemsCatalog;
    public Texture[] imagesCatalog;
    public GameObject defaultItem;
    public Texture defaultTexture;
    private Dictionary<string, GameObject> _itemsCatalog = new Dictionary<string, GameObject>();
    void Start() {
        foreach(GameObject item in itemsCatalog) _itemsCatalog.Add(item.name, item);
    }
    public GameObject getPrefab(string name) {
        GameObject returnValue = defaultItem;
        _itemsCatalog.TryGetValue(name, out returnValue);
        return returnValue;
    }
    public Texture getItemSprite(string name) {
        Texture returnTexture = defaultTexture;
        foreach(Texture sprt in imagesCatalog) if(sprt.name.Equals(name)) returnTexture = sprt;
        foreach(Texture sprt in imagesCatalog) print($"{sprt.name}  <-->  {name}");
        return returnTexture;
    }
}
