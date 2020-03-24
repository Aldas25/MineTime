using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;

    private bool usable = false;

    void Start () {
        UpdateDisplay ();
    }

    void UpdateDisplay () {
        gameObject.GetComponent<SpriteRenderer> ().sprite = item.sprite;
    } 

    public void ChangeItem (Item newItem) {
        item = newItem;
        UpdateDisplay();
    }
}
