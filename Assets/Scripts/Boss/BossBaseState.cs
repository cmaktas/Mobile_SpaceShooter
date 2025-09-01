using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class BossBaseState : MonoBehaviour
{
    protected Camera mainCamera;
    protected float maxLeft, maxRight, maxDown, maxUp, projectionZ;
    protected BossController bossController;

    void Awake()
    {
        mainCamera = Camera.main;
        bossController = GetComponent<BossController>();
    }

    protected virtual void Start()
    {
        StartCoroutine(SetBoundaries());
    }

    public virtual void RunState()
    {

    }
    
    public virtual void StopState()
    {
        StopAllCoroutines();
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
