using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

#if UNITY_EDITOR
using UnityEngine.InputSystem; // TouchSimulation
#endif

/// Enables Enhanced Touch across the app and (in Editor) enables TouchSimulation
public class TouchInputService : MonoBehaviour
{
    private void Awake()
    {

        if (!EnhancedTouchSupport.enabled)    
            EnhancedTouchSupport.Enable();

        #if UNITY_EDITOR
        TouchSimulation.Enable();
        #endif
    }

    private void OnDestroy()
    {
        #if UNITY_EDITOR
        TouchSimulation.Disable();
        #endif

        if (EnhancedTouchSupport.enabled)
        {
            EnhancedTouchSupport.Disable();
        }
    }
}
