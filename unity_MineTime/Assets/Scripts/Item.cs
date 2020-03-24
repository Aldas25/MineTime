using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    //public Sprite sprite;
    public Sprite sprite;
    public int damage;
}
