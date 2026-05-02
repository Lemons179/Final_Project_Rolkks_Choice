using UnityEngine;

public class MovingTilePlatform : MonoBehaviour
{
    public Transform posA, posB; // Empty GameObjects for start/end points
    public float speed = 2f;
    Vector3 targetPos;

    void Start()
    {
        targetPos = posB.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, posA.position) < 0.1f) targetPos = posB.position;
        if (Vector3.Distance(transform.position, posB.position) < 0.1f) targetPos = posA.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}
