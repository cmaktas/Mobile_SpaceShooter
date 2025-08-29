using System.Collections;
using UnityEngine;

public class BossSpecialState : BossBaseState
{
    [SerializeField] float speed, waitTime;
    [SerializeField] GameObject specialBullet;
    [SerializeField] Transform shootingPoint;
    private Vector2 targetPoint;

    protected override void Start()
    {
        base.Start();
        targetPoint = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.9f));
    }

    public override void RunState()
    {
        StartCoroutine(RunSpecialState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    IEnumerator RunSpecialState()
    {
        while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Instantiate(specialBullet, shootingPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        bossController.ChangeState(BossState.Attack);
    }
}
