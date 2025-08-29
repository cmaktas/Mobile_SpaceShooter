using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float parallaxSpeed;
    private SpriteRenderer spriteRenderer;
    private float spriteHight;
    private Vector3 startingPosition;

    void Awake()
    {
        startingPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteHight = spriteRenderer.bounds.size.y;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * parallaxSpeed * Time.deltaTime);
        if (transform.position.y < (startingPosition.y - spriteHight))
            transform.position = startingPosition;
    }
}
