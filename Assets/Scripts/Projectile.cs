using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public bool flip;
    public float damage;
    public float speed;
    private Rigidbody2D rb2D;

    // Called when the gameobject is enabled
    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Called every frame
    public void Update()
    {
        rb2D.velocity = new Vector2(flip ? -speed : speed, 0);
    }

    // Initialize initial values
    public Projectile Init(float pSpeed, bool isFlipped, float dmg)
    {
        speed = pSpeed;
        flip = isFlipped;
        damage = dmg;
        return this;
    }

    // checks for collision with enemy character
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.DecreaseHealth(damage);
        }
        Destroy(gameObject);
    }
}
