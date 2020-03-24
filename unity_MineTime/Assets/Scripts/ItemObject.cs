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
        Sprite newSprite = item==null ? null : item.sprite;
        
        gameObject.GetComponent<SpriteRenderer> ().sprite = newSprite;
    } 

    public void ChangeItem (Item newItem) {
        item = newItem;
        UpdateDisplay();
    }

    public void UseItem () {
        if (item == null)
            return;
        
        usable = true;

        // TODO: use it
    }
}
