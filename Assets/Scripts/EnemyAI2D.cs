using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1f;
    public int damage = 1;

    private Rigidbody2D body;
    private BoxCollider2D box;
    private Animator anim;
    private Transform player;

    private bool movingRight = true;
    private float nextAttackTime = 0f;
    private bool isDead = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (isDead) return;

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

        //THIS CONTROLS IDLE/RUN
        anim.SetFloat("speed", Mathf.Abs(body.velocity.x));
    }

    void Patrol()
    {
        if (!GroundAhead()) Flip();

        float dir = movingRight ? 1 : -1;
        body.velocity = new Vector2(dir * speed, body.velocity.y);
    }

    void Chase()
    {
        float dir = Mathf.Sign(player.position.x - transform.position.x);
        body.velocity = new Vector2(dir * speed, body.velocity.y);

        if ((dir > 0 && !movingRight) || (dir < 0 && movingRight))
        {
            Flip();
        }
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

        GetComponent<Collider2D>().enabled = false;

        anim.SetTrigger("death");

        Destroy(gameObject, 1.2f);
    }

    bool GroundAhead()
    {
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;

        float offset = movingRight ? 0.3f : -0.3f;

        Vector2 c1 = new Vector2(max.x + offset, min.y - 0.1f);
        Vector2 c2 = new Vector2(min.x + offset, min.y - 0.2f);

        return Physics2D.OverlapArea(c1, c2);
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}