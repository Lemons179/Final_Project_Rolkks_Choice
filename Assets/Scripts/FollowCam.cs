using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Object the camera follows.
    public Transform target;
    // Time used to smooth camera movement.
    public float smoothTime = 0.2f;
    // Lowest Y position the camera is allowed to move to.
    public float minY = 0f;

    // Stored velocity used by SmoothDamp.
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // Store the player's Y position as the desired camera Y.
        float targetY = target.position.y;
        // Prevent the camera from going below the minimum Y value.
        targetY = Mathf.Max(targetY, minY);

        // Build the desired camera position while keeping the camera's current Z position.
        Vector3 targetPosition = new Vector3(target.position.x, targetY, transform.position.z);

        // Smoothly move the camera toward the desired position.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
