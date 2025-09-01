using System.Collections;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    [Header("Movement")]
    [SerializeField] private float attackMoveSpeed = 3f;
    [SerializeField] private float positionTolerance = 0.01f;

    [Header("Firing")]
    [SerializeField] private float shootRate = 0.5f;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform[] shootingPoints;

    [Header("Fire State Duration (sec)")]
    [SerializeField] private Vector2 fireStateDurationRange = new(5f, 10f);

    private readonly WaitForEndOfFrame _yieldEndOfFrame = new();

    public override void RunState()
    {
        StartCoroutine(RunFireState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    private IEnumerator RunFireState()
    {
        float shootTimer = 0f;
        float fireStateTimer = 0f;
        float fireStateExitTime = Random.Range(fireStateDurationRange.x, fireStateDurationRange.y);

        Vector2 randomMovePosition = GetRandomMovePosition();

        while (fireStateTimer <= fireStateExitTime)
        {
            randomMovePosition = HandleMovement(randomMovePosition);
            shootTimer = HandleShooting(shootTimer);

            fireStateTimer += Time.deltaTime;
            yield return _yieldEndOfFrame;
        }

        HandleStateExit();
    }

    private Vector2 HandleMovement(Vector2 randomMovePosition)
    {
        if (Vector2.Distance(transform.position, randomMovePosition) > positionTolerance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                randomMovePosition,
                attackMoveSpeed * Time.deltaTime
            );
        }
        else
        {
            randomMovePosition = GetRandomMovePosition();
        }
        return randomMovePosition;
    }

    private float HandleShooting(float shootTimer)
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootRate)
        {
            for (int i = 0; i < shootingPoints.Length; i++)
            {
                Instantiate(enemyBullet, shootingPoints[i].position, Quaternion.identity);
            }
            shootTimer = 0f;
        }
        return shootTimer;
    }

    private void HandleStateExit()
    {
        bossController.ChangeState(BossState.Special);
    }

    private Vector2 GetRandomMovePosition()
    {
        return new Vector2(Random.Range(maxLeft, maxRight), Random.Range(maxDown, maxUp));
    }
}
