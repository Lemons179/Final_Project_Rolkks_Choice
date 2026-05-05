using UnityEngine;

public class FitBackgroundToCamera : MonoBehaviour
{
    // Camera whose view the background must cover.
    public Camera targetCamera;

    // Extra size added to prevent small gaps at screen edges.
    public float padding = 1.1f;

    // Original local scale of the background group.
    private Vector3 originalScale;

    // Last camera aspect ratio used for fitting.
    private float lastAspect;

    void Start()
    {
        // Use the main camera if no camera is assigned.
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        // Store the starting scale so resizing stays based on the original art size.
        originalScale = transform.localScale;

        // Fit the background when the scene starts.
        FitToCamera();
    }

    void Update()
    {
        // Refit the background if the screen aspect ratio changes.
        if (!Mathf.Approximately(lastAspect, targetCamera.aspect))
        {
            FitToCamera();
        }
    }

    void FitToCamera()
    {
        // Store the current aspect ratio.
        lastAspect = targetCamera.aspect;

        // Reset scale before measuring the background bounds.
        transform.localScale = originalScale;

        // Get all SpriteRenderers in this background group.
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        // Stop if the background group has no sprites.
        if (renderers.Length == 0)
        {
            return;
        }

        // Start the bounds using the first sprite renderer.
        Bounds backgroundBounds = renderers[0].bounds;

        // Expand the bounds to include every child sprite.
        for (int i = 1; i < renderers.Length; i++)
        {
            backgroundBounds.Encapsulate(renderers[i].bounds);
        }

        // Calculate the camera height in world units.
        float cameraHeight = targetCamera.orthographicSize * 2f;

        // Calculate the camera width in world units.
        float cameraWidth = cameraHeight * targetCamera.aspect;

        // Calculate the scale required to cover the camera width.
        float scaleX = cameraWidth / backgroundBounds.size.x;

        // Calculate the scale required to cover the camera height.
        float scaleY = cameraHeight / backgroundBounds.size.y;

        // Use the larger scale so the background covers the whole camera view.
        float finalScale = Mathf.Max(scaleX, scaleY) * padding;

        // Apply uniform scaling to avoid stretching the art.
        transform.localScale = originalScale * finalScale;

        // Keep the background centered behind the camera.
        transform.localPosition = new Vector3(0f, 0f, transform.localPosition.z);
    }
}