using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    public List<InventoryItem> items =
        new List<InventoryItem>();

    public void Init(Inventory template)
    {
        items.Clear();
        foreach(InventoryItem item in template.items)
        {
            items.Add(item);
        }
    }

    public Inventory GetInstance()
    {
        Inventory instance = CreateInstance(typeof(Inventory)) as Inventory;
        instance.Init(this);
        return instance;
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        if (ContainsItem(item))
        {
            items.Remove(item);
        }
    }

    public void DropItem(InventoryItem inventoryItem, Vector3 location, Quaternion rotation)
    {
        if (ContainsItem(inventoryItem))
        {
            items.Remove(inventoryItem);
            Instantiate(inventoryItem.item, location, rotation);
        }
    }

    public void DropAllItems(Vector3 location, Quaternion rotation)
    {
        foreach(InventoryItem inventoryItem in items)
        {
            if (!inventoryItem || !inventoryItem.item)
                continue;

            Instantiate(inventoryItem.item, location, rotation);
        }
    }

    public bool ContainsItem(InventoryItem item)
    {
        return items.Contains(item) ? true : false;
    }
}