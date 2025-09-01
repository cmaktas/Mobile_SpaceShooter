using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss States")]
    [SerializeField] private BossEnterState bossEnterState;
    [SerializeField] private BossAttackState bossAttack;
    [SerializeField] private BossSpecialState bossSpecialState;
    [SerializeField] private BossDeathState bossDeathState;

    [Header("Testing")]
    [SerializeField] private bool test;
    [SerializeField] private BossState testState;

    void Start()
    {
        ChangeState(BossState.Enter);
        if (test)
            ChangeState(testState);
    }

    public void ChangeState(BossState state)
    {
        Debug.Log("Boss Controller: Boss entered switch state: " + state);
        switch (state)
        {
            case BossState.Enter:
                bossEnterState.RunState();
                break;
            case BossState.Attack:
                bossAttack.RunState();
                break;
            case BossState.Special:
                bossSpecialState.RunState();
                break;
            case BossState.Death:
                bossEnterState.StopState();
                bossAttack.StopState();
                bossSpecialState.StopState();
                bossDeathState.RunState();
                break;
        }
    }

}
