using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Item", order = 1)]
public class InventoryItem : ScriptableObject
{
    public new string name;
    public Sprite image;
    public Item item;
}
