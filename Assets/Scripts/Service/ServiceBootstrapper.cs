using System;
using System.Collections.Generic;
using UnityEngine;

/// Automatically instantiates and initializes core runtime services without scene dependencies.
public static class ServiceBootstrapper
{
    private static readonly List<Type> ServiceTypes = new()
    {
        typeof(CameraBoundsService),
        typeof(TouchInputService)
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeAllServices()
    {
        Debug.Log("[ServiceBootstrapper] Starting service orchestration...");

        foreach (Type serviceType in ServiceTypes)
        {
            OrchestrateServiceCreation(serviceType);
        }

        Debug.Log("[ServiceBootstrapper] Service orchestration complete.");
    }

    /// Orchestrates the verification and creation process for a given service type.
    private static void OrchestrateServiceCreation(Type serviceType)
    {
        if (!IsMonoBehaviour(serviceType)) return;
        if (IsServiceAlreadyExists(serviceType)) return;
        CreateAndRegisterService(serviceType);
    }

    /// Verifies if the given type is a MonoBehaviour.
    private static bool IsMonoBehaviour(Type type)
    {
        if (!typeof(MonoBehaviour).IsAssignableFrom(type))
        {
            Debug.LogError($"[ServiceBootstrapper] Type {type.Name} is not a MonoBehaviour. Skipping...");
            return false;
        }

        return true;
    }

    /// Checks if an instance of the service already exists in the scene.
    private static bool IsServiceAlreadyExists(Type type)
    {
        // Use the newer, faster Unity API
        UnityEngine.Object existing = UnityEngine.Object.FindAnyObjectByType(type);
        if (existing != null)
        {
            Debug.Log($"[ServiceBootstrapper] Service already exists: {type.Name}");
            return true;
        }
        return false;
    }

    /// Creates a new GameObject and attaches the service component to it.
    private static void CreateAndRegisterService(Type type)
    {
        GameObject serviceGO = new GameObject(type.Name);
        serviceGO.AddComponent(type);
        UnityEngine.Object.DontDestroyOnLoad(serviceGO);

        Debug.Log($"[ServiceBootstrapper] Created and registered service: {type.Name}");
    }
}
