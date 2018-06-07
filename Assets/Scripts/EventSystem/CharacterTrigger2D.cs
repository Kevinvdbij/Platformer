using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class CharacterTrigger2D : MonoBehaviour
{
    public UnityCharacterEvent triggerEvent;
    public bool disableOnTrigger;
    public bool playerOnly;
    private Collider2D col;

    public void Start()
    {
        col = GetComponent<Collider2D>();
        if (col)
            col.isTrigger = true;

        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerOnly)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (!player)
                return;
        }

        triggerEvent.Invoke(DummyCharacter.NewDummy);
        if (disableOnTrigger)
            gameObject.SetActive(false);
    }
}
