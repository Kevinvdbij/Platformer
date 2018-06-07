using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    ALLY,
    NEUTRAL,
    ENEMY
}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [Header("Health")]
    public Health healthAsset;
    public bool createHealthInstance = true;
    public bool setMaxHealthOnSpawn;
    protected Health health;

    [Header("Inventory")]
    public Inventory inventoryAsset;
    public bool createInventoryInstance = true;
    protected Inventory inventory;

    [Header("Combat")]
    public CharacterType type = CharacterType.NEUTRAL;
    public float damage;

    [Header("Animation")]
    protected Animator animator;
    protected SpriteRenderer spriteRend;
    public bool isFlipped = false;

    [Header("Physics")]
    protected Rigidbody2D rb2D;
    protected BoxCollider2D col2D;
    protected bool isGrounded;
    protected Collider2D groundedCol;

    [Header("Character Events")]
    public UnityCharacterEvent spawn;
    public UnityCharacterEvent despawn;
    public UnityCharFloatEvent onIncreaseHealth;
    public UnityCharFloatEvent onDecreaseHealth;
    public UnityCharacterEvent onAddItem;
    public UnityCharacterEvent onRemoveItem;

    // Gets called every frame
    protected virtual void Update()
    {
        UpdateAnimation();
    }

    // Initialized intial variables
    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<BoxCollider2D>();

        if (healthAsset)
        {
            health = createHealthInstance ? healthAsset.GetInstance(setMaxHealthOnSpawn) : healthAsset.Init(setMaxHealthOnSpawn);
        }

        if (inventoryAsset)
        {
            inventory = createInventoryInstance ? inventoryAsset.GetInstance() : inventoryAsset;
        }

        if (spawn != null)
        {
            spawn.Invoke(this);
        }
    }

    // Called on despawn
    protected virtual void Uninit()
    {
        if (despawn != null)
            despawn.Invoke(this);
    }

    // Called when the gameobject is enabled
    private void OnEnable()
    {
        Init();
    }

    // Called when the gameobject is disabled
    private void OnDisable()
    {
        Uninit();
    }

    // Called when the character dies
    protected virtual void OnDeath()
    {
        inventory.DropAllItems(transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    // Handles increasing character health value
    public void IncreaseHealth(float amount)
    {
        if (!health)
            return;

        health.Increase(amount);
        if(onDecreaseHealth != null)
            onIncreaseHealth.Invoke(this, amount);
    }

    // Handles decreasing character health value
    public void DecreaseHealth(float amount)
    {
        if (health)
            health.Decrease(amount);

        if (onDecreaseHealth != null)
            onDecreaseHealth.Invoke(this, amount);

        if (health.IsDepleted)
            OnDeath();
    }

    // Handles adding an item to the character inventory
    public void AddItem(InventoryItem item)
    {
        if (!inventory)
            return;

        inventory.AddItem(item);
        if (onAddItem != null)
            onAddItem.Invoke(this);
    }

    // Handles removing an item from the character inventory
    public void RemoveItem(InventoryItem item)
    {
        if (!inventory)
            return;

        inventory.RemoveItem(item);
        if (onRemoveItem != null)
            onRemoveItem.Invoke(this);
    }

    // Checks if the character can damage another character
    protected virtual bool CanDamage(Character character)
    {
        if (character.type == CharacterType.ENEMY)
            return true;
        else
            return false;
    }

    // Updates the animator variables with correct values
    protected virtual void UpdateAnimation()
    {
        animator.SetBool("isMoving", IsMoving);
        spriteRend.flipX = IsFlipped;
    }

    // Checks if the character is dead
    protected virtual bool IsDead
    {
        get
        {
            return health ? health.IsDepleted : true;
        }
    }

    // Checks if the character is moving
    protected virtual bool IsMoving
    {
        get
        {
            return false;
        }
    }

    // Checks if the character sprite is flipped
    protected virtual bool IsFlipped
    {
        get
        {
            return isFlipped;
        }
    }

    // Checks if the character is grounded to a collider
    protected virtual bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    }

    public void OnKillBarrier()
    {
        health.current = health.minimum;
        OnDeath();
    }

    // Called when the collider receives a collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D cont = System.Array.Find(collision.contacts, x => x.normal.y == 1);
        if (cont.collider != null)
        {
            isGrounded = true;
            groundedCol = cont.collider;
        }
    }

    // Called when collider leaves collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == groundedCol)
        {
            isGrounded = false;
            groundedCol = null;
        }
    }
}