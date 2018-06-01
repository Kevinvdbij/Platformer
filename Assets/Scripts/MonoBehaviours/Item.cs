using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public InventoryItem itemAsset;
    public bool isPickup = true;
    public bool disableOnPickup = true;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character)
            OnPickup(character);
    }

    public void OnPickup(Character character)
    {
        character.AddItem(itemAsset);
        if (disableOnPickup)
            gameObject.SetActive(false);
    }
}
