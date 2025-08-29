using UnityEngine;

/// Parent-driven vertical parallax.
[DisallowMultipleComponent]
public class VerticalParallax2D : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float parallaxSpeed = 3f;

    [Header("Images")]
    [SerializeField] private SpriteRenderer topImageSR;
    [SerializeField] private SpriteRenderer bottomImageSR;

    private float spriteHight;
    private Vector3 startingPosition;

    private void Awake()
    {
        CacheMeasurements();
    }

    private void Update()
    {
        Vector3 delta = parallaxSpeed * Time.deltaTime * Vector3.down;

        MoveImages(delta);

        // When bottom moved one height below its own start, snap above the top
        if (bottomImageSR.transform.position.y < (startingPosition.y - spriteHight))
        {
            bottomImageSR.transform.position = new Vector3(
                bottomImageSR.transform.position.x,
                topImageSR.transform.position.y + spriteHight,
                bottomImageSR.transform.position.z
            );

            // Swap roles: the one we just moved up is now the new 'top'
            (topImageSR, bottomImageSR) = (bottomImageSR, topImageSR);

            // Reset the new bottom's start position for the next cycle
            startingPosition = bottomImageSR.transform.position;
        }
    }

    /// Caches tile height and bottom image's initial position.
    private void CacheMeasurements()
    {
        spriteHight = bottomImageSR.bounds.size.y;
        startingPosition = bottomImageSR.transform.position;
    }

    private void MoveImages(Vector3 delta)
    {
        topImageSR.transform.position += delta;
        bottomImageSR.transform.position += delta;
    }

}
