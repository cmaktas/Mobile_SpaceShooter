using System.Collections;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    [SerializeField] private float attackMoveSpeed;
    [SerializeField] private float shootRate;
    [SerializeField] private GameObject enemyBullet;
    [Header("Shooting Points")]
    [SerializeField] private Transform[] shootingPoints;

    public override void RunState()
    {
        StartCoroutine(RunFireState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    IEnumerator RunFireState()
    {
        float shootTimer = 0f;
        float fireStateTimer = 0f;
        float fireStateExitTime = Random.Range(5f, 10f);
        Vector2 randomMovePosition = new Vector2(Random.Range(maxLeft, maxRight), Random.Range(maxDown, maxUp));

        while (fireStateTimer <= fireStateExitTime)
        {
            if (Vector2.Distance(transform.position, randomMovePosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, randomMovePosition, attackMoveSpeed * Time.deltaTime);
            }
            else
            {
                randomMovePosition = new Vector2(Random.Range(maxLeft, maxRight), Random.Range(maxDown, maxUp));
            }
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootRate)
            {
                for (int i = 0; i < shootingPoints.Length; i++)
                {
                    Instantiate(enemyBullet, shootingPoints[i].position, Quaternion.identity);
                }
                shootTimer = 0;
            }
            yield return new WaitForEndOfFrame();
            fireStateTimer += Time.deltaTime;
        }
        bossController.ChangeState(BossState.Special);
        /* int randomPick = Random.Range(0,2);
        if (randomPick == 0)
        {
            bossController.ChangeState(BossState.Attack);
        }
        else
        {
            bossController.ChangeState(BossState.Special);
        } */
    }

}
