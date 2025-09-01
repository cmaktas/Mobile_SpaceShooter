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

    void Update()
    {
        if (Touch.activeTouches.Count == 0)
            return;

        if (Touch.activeTouches.Count > 0)
        {
            ProcessPrimaryTouch();
            ClampWithinBounds();
        }
    }


    private void ProcessPrimaryTouch()
    {
        if (Touch.activeTouches[0].finger.index != 0)
            return;

        Touch playerTouch = Touch.activeTouches[0];

        HandleDoubleTap(playerTouch);

        Vector3 screenPoint = BuildScreenPoint(playerTouch);

        #if UNITY_EDITOR
        if (screenPoint.x == Mathf.Infinity) return;
        #endif

        Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(screenPoint);

        var phase = playerTouch.phase;

        BeginDragIfNeeded(phase, touchWorldPosition);
        DragIfNeeded(phase, touchWorldPosition);
    }

    private void HandleDoubleTap(Touch playerTouch)
    {
        if (playerTouch.tapCount == 2)
        {
            Debug.Log("Player tapped the screen");
        }
    }

    private Vector3 BuildScreenPoint(Touch playerTouch)
    {
        return new Vector3(playerTouch.screenPosition.x, playerTouch.screenPosition.y, projectionZ);
    }

    private void BeginDragIfNeeded(TouchPhase phase, Vector3 touchWorldPosition)
    {
        if (phase == TouchPhase.Began)
        {
            offset = touchWorldPosition - transform.position;
        }
    }

    private void DragIfNeeded(TouchPhase phase, Vector3 touchWorldPosition)
    {
        if (phase == TouchPhase.Moved || phase == TouchPhase.Stationary)
        {
            transform.position = new Vector3(
                touchWorldPosition.x - offset.x,
                touchWorldPosition.y - offset.y,
                0
            );
        }
    }

    private void ClampWithinBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, maxLeft, maxRight),
            Mathf.Clamp(transform.position.y, maxDown, maxUp),
            0
        );
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
