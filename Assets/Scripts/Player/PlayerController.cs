using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference inputActionReference;
    [SerializeField] private float speed; 

    private Camera mainCamera;
    private Vector3 offset;
    private float maxLeft, maxRight, maxDown, maxUp, projectionZ;

    private InputAction moveAction;

    void Awake()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundaries());
    }

    private void OnEnable()
    {
        moveAction = inputActionReference != null ? inputActionReference.action : null;
        moveAction?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
    }

    /* void Update()
    {
        Vector2 moveDirection = inputActionReference.action.ReadValue<Vector2>();
        transform.Translate(moveDirection * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxLeft, maxRight),
                                            Mathf.Clamp(transform.position.y, maxDown, maxUp), 0);
    } */

    void Update()
    {
        if (Touch.activeTouches.Count == 0)
            return;

        if (Touch.activeTouches.Count > 0)
        {
            if (Touch.activeTouches[0].finger.index == 0)
            {
                Touch playerTouch = Touch.activeTouches[0];

                if (playerTouch.tapCount == 2)
                {
                    Debug.Log("Player tapped the screen");
                } 

                Vector3 screenPoint = new(playerTouch.screenPosition.x, playerTouch.screenPosition.y, projectionZ);

                #if UNITY_EDITOR
                if (screenPoint.x == Mathf.Infinity) return;
                #endif
                
                Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(screenPoint);

                var phase = playerTouch.phase;

                if (phase == TouchPhase.Began)
                {
                    offset = touchWorldPosition - transform.position;
                }

                if (phase == TouchPhase.Moved || phase == TouchPhase.Stationary)
                {
                    transform.position = new Vector3(touchWorldPosition.x - offset.x, touchWorldPosition.y - offset.y, 0);
                }
            }
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxLeft, maxRight),
                                            Mathf.Clamp(transform.position.y, maxDown, maxUp), 0);
        }
    }

    private IEnumerator SetBoundaries()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.4f);
        projectionZ = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        maxLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.15f, 0f, projectionZ)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector3(0.85f, 0f, projectionZ)).x;
        maxDown = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.05f, projectionZ)).y;
        maxUp = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.8f, projectionZ)).y;
    }

}
