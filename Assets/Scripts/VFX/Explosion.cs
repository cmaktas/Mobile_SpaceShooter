using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 0.8f);
    }

}
