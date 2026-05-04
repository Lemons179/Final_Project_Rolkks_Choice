using UnityEngine;

public class MovingTilePlatform : MonoBehaviour
{
    // Endpoints of the platform path.
    public Transform posA, posB;

    // Time required to move from one endpoint to the other.
    public float moveTime = 3f;

    // Time spent paused at each endpoint.
    public float waitTime = 1f;

    // Offset applied to the shared movement cycle.
    public float timeOffset = 0f;

    // Determines whether movement begins from posB instead of posA.
    public bool startAtB = false;

    void Update()
    {
        // Total duration of one full back-and-forth cycle.
        float cycleTime = (waitTime * 2f) + (moveTime * 2f);

        // Current time position inside the repeating cycle.
        float timer = (Time.time + timeOffset) % cycleTime;

        // Select starting and ending positions based on starting side.
        Vector3 start = startAtB ? posB.position : posA.position;
        Vector3 end = startAtB ? posA.position : posB.position;

        // Hold at the starting endpoint.
        if (timer < waitTime)
        {
            transform.position = start;
        }
        // Move from start to end.
        else if (timer < waitTime + moveTime)
        {
            float moveProgress = (timer - waitTime) / moveTime;
            transform.position = Vector3.Lerp(start, end, moveProgress);
        }
        // Hold at the ending endpoint.
        else if (timer < waitTime + moveTime + waitTime)
        {
            transform.position = end;
        }
        // Move from end back to start.
        else
        {
            float moveProgress = (timer - waitTime - moveTime - waitTime) / moveTime;
            transform.position = Vector3.Lerp(end, start, moveProgress);
        }

        // Draw the platform path in the Scene view.
        Debug.DrawLine(posA.position, posB.position, Color.red);
    }
}