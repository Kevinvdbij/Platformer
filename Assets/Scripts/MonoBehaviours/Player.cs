using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player")]
    public float movementSpeed;
    public float jumpPower;

    [Header("Projectile")]
    public float projectileSpeed;
    public Projectile projectile;
    public Vector2 projOffset;
    public Transform emitter;

    private Vector2 originEmitterPos;

    // Fixed update to handle physics calculations
    private void FixedUpdate()
    {
        Movement();  
    }

    // Initialization of variables
    protected override void Init()
    {
        base.Init();
        originEmitterPos = emitter.localPosition;
    }

    // Handles player input and chracter movement
    private void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb2D.AddForce((Vector2.right * movementSpeed) * moveHorizontal, ForceMode2D.Impulse);

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        }

        if (rb2D.velocity.x > movementSpeed)
        {
            rb2D.velocity = new Vector2(movementSpeed, rb2D.velocity.y);
        }
        else if (rb2D.velocity.x < -movementSpeed)
        {
            rb2D.velocity = new Vector2(-movementSpeed, rb2D.velocity.y);
        }

        if (moveHorizontal == 0)
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    // Updates animator variables
    protected override void UpdateAnimation()
    {
        base.UpdateAnimation();
        animator.SetBool("isCrouching", IsCrouching);

        if (Input.GetButtonDown("Shoot"))
        {
            animator.SetTrigger("Shoot");
        }
    }

    // Spawns and initializes player fired projectile
    public void SpawnProjectile()
    {
        Vector3 position = new Vector3(emitter.position.x + (IsFlipped ? -projOffset.x : projOffset.x), emitter.position.y + projOffset.y, emitter.position.z);
        Instantiate(projectile, position, emitter.rotation, null).Init(projectileSpeed, IsFlipped, damage);
    }

    // Checks if the player is giving horizontal input
    protected override bool IsMoving
    {
        get
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Checks and flips the character sprite when appropriate
    protected override bool IsFlipped
    {
        get
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                isFlipped = true;
                emitter.localPosition = new Vector2(-originEmitterPos.x, originEmitterPos.y);
                emitter.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                isFlipped = false;
                emitter.localPosition = originEmitterPos;
                emitter.GetComponent<SpriteRenderer>().flipX = false;
            }
            return isFlipped;
        }
    }

    // Checks if the player character is crouching
    protected bool IsCrouching
    {
        get
        {
            return Input.GetAxisRaw("Vertical") < 0 ? true : false;
        }
    }
}
