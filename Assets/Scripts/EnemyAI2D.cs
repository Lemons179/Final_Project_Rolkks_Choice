using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//KILLER QUEEN - QUEEN (Technically the enemies are a killer queen)
public class EnemyAI2D : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRange = 7f;
    public float attackRange = 1.2f;

    public float jumpForce = 8f;
    public float jumpCheckDistance = 1f;
    public float jumpCooldown = 0.8f;

    public int damage = 1;
    public float attackCooldown = 1f;

    public float startIdleTime = 1f;

    private Rigidbody2D body;
    private BoxCollider2D box;
    private Animator anim;
    private Transform player;

    private bool movingRight = true;
    private bool isDead = false;

    private float nextAttackTime = 0f;
    private float nextJumpTime = 0f;
    private float startMoveTime;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        startMoveTime = Time.time + startIdleTime;
    }

    void Update()
    {
        if (isDead) return;

        if (Time.time < startMoveTime)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetFloat("speed", 0);
            return;
        }

        float distance = player != null
            ? Vector2.Distance(transform.position, player.position)
            : Mathf.Infinity;

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= detectionRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }

        anim.SetFloat("speed", Mathf.Abs(body.velocity.x));
    }

    void Patrol()
    {
        if (!GroundAhead() || WallAhead())
        {
            Flip();
            return;
        }

        float direction = movingRight ? 1f : -1f;
        body.velocity = new Vector2(direction * patrolSpeed, body.velocity.y);
    }

    void Chase()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        body.velocity = new Vector2(direction * chaseSpeed, body.velocity.y);

        if ((direction > 0 && !movingRight) || (direction < 0 && movingRight))
        {
            Flip();
        }

        bool playerIsHigher = player.position.y > transform.position.y + 0.8f;

        if (!GroundAhead() || WallAhead() || playerIsHigher)
        {
            TryJump();
        }
    }

    void TryJump()
    {
        if (!IsGrounded()) return;
        if (Time.time < nextJumpTime) return;

        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        nextJumpTime = Time.time + jumpCooldown;
    }

    void Attack()
    {
        body.velocity = new Vector2(0, body.velocity.y);

        if (Time.time >= nextAttackTime && !IsPlayingAttack())
        {
            anim.SetTrigger("attack");

            PlayerHealth2D health = player.GetComponent<PlayerHealth2D>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    bool IsGrounded()
    {
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;

        Vector2 c1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 c2 = new Vector2(min.x, min.y - 0.2f);

        Collider2D hit = Physics2D.OverlapArea(c1, c2);
        return hit != null && hit.gameObject != gameObject;
    }

    bool GroundAhead()
    {
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;

        float offset = movingRight ? 0.4f : -0.4f;

        Vector2 c1 = new Vector2(max.x + offset, min.y - 0.1f);
        Vector2 c2 = new Vector2(min.x + offset, min.y - 0.2f);

        Collider2D hit = Physics2D.OverlapArea(c1, c2);
        return hit != null && hit.gameObject != gameObject;
    }

    bool WallAhead()
    {
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;

        float dir = movingRight ? 1f : -1f;

        Vector2 origin = new Vector2(
            movingRight ? max.x : min.x,
            transform.position.y
        );

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * dir, jumpCheckDistance);
        return hit.collider != null && hit.collider.gameObject != gameObject;
    }

    bool IsPlayingAttack()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        return info.IsName("Mushroom_Attack");
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        body.velocity = Vector2.zero;
        body.gravityScale = 0;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        anim.SetTrigger("death");

        Destroy(gameObject, 1.2f);
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}