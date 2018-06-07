using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    IDLE,
    PATROL,
    ATTACK
}

public class Enemy : Character
{
    [Header("Enemy")]
    public EnemyState state;
    private Player player;
    private bool canAttack = true;
    [SerializeField] private LayerMask detectMask;
    [SerializeField] private float attackDistance;
    [SerializeField] private float patrolDistance;
    [SerializeField] private float detectDistance;
    [SerializeField] private List<Transform> waypoints;
    private int waypointIndex = 0;

    protected override void Update()
    {
        base.Update();
        EnemyBehaviour();
    }

    protected override void Init()
    {
        base.Init();
        StartCoroutine(DetectPlayer());
    }

    protected override void UpdateAnimation()
    {
        base.UpdateAnimation();
        animator.SetBool("isMoving", rb2D.velocity.x > 0.2f || rb2D.velocity.x < -0.2f ? true : false);
    }

    private void EnemyBehaviour()
    {
        switch(state)
        {
            case EnemyState.ATTACK:
                AttackBehaviour();
                break;

            case EnemyState.PATROL:
                PatrolBehaviour();
                break;
        }
    }

    private void AttackBehaviour()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
        {
            Vector2 velocity = GetDirection(player.transform);
            rb2D.velocity = velocity;
        }
        else if (canAttack)
        {
            animator.SetTrigger("isAttacking");
            canAttack = false;
            StartCoroutine(AttackCooldown(5f));
        }
    }

    private void PatrolBehaviour()
    {
        if (waypoints[0] == null)
            state = EnemyState.IDLE;

        if (Vector2.Distance(transform.position, waypoints[WaypointIndex].position) > patrolDistance)
        {
            Vector2 velocity = GetDirection(waypoints[WaypointIndex]);
            rb2D.velocity = velocity;
        }
        else
        {
            WaypointIndex++;
        }
    }

    public void OnAttack()
    {
        player.DecreaseHealth(damage);
    }

    protected override void OnDeath()
    {
        inventory.DropAllItems(transform.position, transform.rotation);
        animator.SetBool("isDead", true);
        gameObject.layer = LayerMask.NameToLayer("DeadCharacter");
        col2D.enabled = false;
        rb2D.isKinematic = true;
        rb2D.velocity = Vector2.zero;
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.13f);
        Destroy(this);
    }

    private IEnumerator DetectPlayer()
    {
        while (true)
        {
            RaycastHit2D hit = (Physics2D.Raycast(transform.position, IsFlipped ? -transform.right : transform.right, detectDistance, detectMask));
            if (hit)
            {
                Player p = hit.transform.GetComponent<Player>();
                if (p)
                {
                    player = p;
                    state = EnemyState.ATTACK;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator AttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    protected override bool CanDamage(Character character)
    {
        if (character.type == CharacterType.ALLY)
            return true;
        else
            return false;
    }

    private Vector2 GetDirection(Transform target)
    {
        if (!target)
            return new Vector2(0, 0);

        if (transform.position.x < target.position.x)
        {
            return new Vector2(1, 0);
        }
        else if (transform.position.x > target.position.x)
        {
            return new Vector2(-1, 0);
        }
        else return new Vector2(0, 0);
    }

    private int WaypointIndex
    {
        get
        {
            return waypointIndex;
        }

        set
        {
            if (value > waypoints.Count - 1)
            {
                waypointIndex = 0;
            }
            else
            {
                waypointIndex = value;
            }
        }
    }

    protected override bool IsFlipped
    {
        get
        {
            if (rb2D.velocity.x > 0.2f)
            {
                isFlipped = false;
                return false;
            }
            else if (rb2D.velocity.x < -0.2f)
            {
                isFlipped = true;
                return true;
            }
            else
            {
                return isFlipped;
            }
        }
    }
}
