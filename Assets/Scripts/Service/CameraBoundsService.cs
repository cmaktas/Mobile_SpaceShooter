using UnityEngine;

/// Calculates and exposes the world bounds of the main camera.
/// Use this to constrain player movement or control spawn regions.
public class CameraBoundsService : MonoBehaviour
{
    public static CameraBoundsService Instance { get; private set; }

    [SerializeField] private Camera mainCamera;

    public Vector2 MinBounds { get; private set; }
    public Vector2 MaxBounds { get; private set; }

    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Wait for all objects to be ready before assigning main camera to the service
    private void Start()
    {
        if (mainCamera is null)
            mainCamera = Camera.main;

        if (mainCamera is null)
        {
            Debug.LogWarning("[CameraBoundsService] Main camera not found in Start.");
            return;
        }

        UpdateBounds();
    }

    // Sync with camera position changes each frame
    private void LateUpdate()
    {
        if (mainCamera == null) return;
        UpdateBounds();
    }

    /// Updates the camera view bounds.
    private void UpdateBounds()
    {
        MinBounds = (Vector2)mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, mainCamera.nearClipPlane));
        MaxBounds = (Vector2)mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, mainCamera.nearClipPlane));
    }

    /// Clamps a given position to stay within the camera view bounds.
    public Vector2 ClampPositionToBounds(Vector2 position, Vector2 extent)
    {
        float clampedX = Mathf.Clamp(position.x, MinBounds.x + extent.x, MaxBounds.x - extent.x);
        float clampedY = Mathf.Clamp(position.y, MinBounds.y + extent.y, MaxBounds.y - extent.y);

        return new Vector2(clampedX, clampedY);
    }

}
