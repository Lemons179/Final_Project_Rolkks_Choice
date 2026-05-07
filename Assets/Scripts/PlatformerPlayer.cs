using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformerPlayer : MonoBehaviour
{

    // Position where the player returns after falling off the map.
    public Transform respawnPoint;

    // Y position below which the player is considered out of bounds.
    public float fallLimitY = -20f;

    public float speed = 4.5f;
    public float jumpForce = 12f;
    public int extraJumpsMax = 1;

    public float dashSpeed = 14f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.LeftShift;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;
    private int extraJumpsRemaining;

    private bool isDashing = false;
    private bool hasAirDashed = false;
    private float facingDirection = 1f;
    private float nextDashTime = 0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isDashing) return;

        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        anim.SetFloat("speed", Mathf.Abs(deltaX));

        if (!Mathf.Approximately(deltaX, 0))
        {
            facingDirection = Mathf.Sign(deltaX);
            transform.localScale = new Vector3(facingDirection, 1, 1);
        }

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if (hit != null)
        {
            grounded = true;
        }

        MovingTilePlatform platform = null;
        if (grounded)
        {
            platform = hit.GetComponent<MovingTilePlatform>();
        }

        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        Vector3 playerScale = Vector3.one;
        if (platform != null)
        {
            playerScale = platform.transform.localScale;
        }

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(
                facingDirection / playerScale.x,
                1 / playerScale.y,
                1
            );
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        if (grounded)
        {
            extraJumpsRemaining = extraJumpsMax;
            hasAirDashed = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (extraJumpsRemaining > 0)
            {
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                extraJumpsRemaining--;
            }
        }

        if (Input.GetKeyDown(dashKey) && CanDash(grounded))
        {
            StartCoroutine(Dash(grounded));
        }
    }

    bool CanDash(bool grounded)
    {
        if (Time.time < nextDashTime)
        {
            return false;
        }

        if (!grounded && hasAirDashed)
        {
            return false;
        }

        return true;
    }

    IEnumerator Dash(bool grounded)
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;

        if (!grounded)
        {
            hasAirDashed = true;
        }

        float originalGravity = body.gravityScale;
        body.gravityScale = 0;

        body.velocity = new Vector2(facingDirection * dashSpeed, 0);

        yield return new WaitForSeconds(dashDuration);

        body.gravityScale = originalGravity;
        isDashing = false;
        // Respawn the player if they fall below the level.
        CheckFallRespawn();
    }

    private void CheckFallRespawn()
    {
        // Check whether the player has fallen below the allowed Y position.
        if (transform.position.y < fallLimitY)
        {
            // Stop the player's current movement before teleporting.
            body.velocity = Vector2.zero;

            // Move the player back to the respawn point if one is assigned.
            if (respawnPoint != null)
            {
                transform.position = respawnPoint.position;
            }
        }
    }
}