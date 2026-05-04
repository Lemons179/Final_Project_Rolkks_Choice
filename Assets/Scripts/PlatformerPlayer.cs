using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformerPlayer : MonoBehaviour
{

    public float speed = 4.5f;
    public float jumpForce = 12f;
    public int extraJumpsMax = 1; // Number of extra jumps (1 for double jump)
    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;
    private int extraJumpsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        anim.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
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
        if(!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / playerScale.x, 1 / playerScale.y, 1);
        }

        // Stops the player from slipping on steep slopes.
        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;


        // Reset jumps when on the ground
        if (grounded)
        {
            extraJumpsRemaining = extraJumpsMax;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (extraJumpsRemaining > 0)
            {
                // Double Jump
                body.velocity = new Vector2(body.velocity.x, 0); // Cancel falling speed before jumping
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                extraJumpsRemaining--;
            }
        }
        
    }
}
