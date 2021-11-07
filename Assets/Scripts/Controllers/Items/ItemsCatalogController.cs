using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCatalogController : MonoBehaviour
{
    public GameObject[] itemsCatalog;
    private Dictionary<string, GameObject> _itemsCatalog = new Dictionary<string, GameObject>();
    void Start() {
        foreach(GameObject item in itemsCatalog) _itemsCatalog.Add(item.name, item);
    }
    public GameObject getPrefab(string name) {
        GameObject returnValue = null;
        _itemsCatalog.TryGetValue(name, out returnValue);
        return returnValue;
    }
}
