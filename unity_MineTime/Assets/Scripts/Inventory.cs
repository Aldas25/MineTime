using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int size = 0;
    private Item [] items;

    void Awake () {
        UpdateSize(3);
    }

    public void UpdateSize (int newSize) {
        Item[] newArray = new Item[newSize];
        for (int i = 0; i < size; i++)
            newArray[i] = items[i];
        size = newSize;
        items = newArray;
    }

    public void CollectItem (GameObject itemToCollect) {
        if (!HasEmptySpots()) 
            return;
        
        Item newItem = itemToCollect.GetComponent<ItemObject>().item;
        AddItem (newItem);
        Destroy(itemToCollect);
    }

    bool HasEmptySpots () {
        for (int i = 0; i < size; i++)
            if (items[i] == null)
                return true;
        return false;
    }

    void AddItem (Item newItem) {
        for (int i = 0; i < size; i++) {
            if (items[i] == null) {
                items[i] = newItem;
                return;
            }
        }
    }

    public void RemoveItem (int id) {
        items[id] = null;
    }

    public Item GetItem (int id) {
        if (id >= size || items[id] == null)
            return null;
        return items[id];
    }

}
