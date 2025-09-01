public class BossDeathState : BossBaseState
{

    public override void RunState()
    {
        EndGameManager.endGameManager.possibleWin = true;
        EndGameManager.endGameManager.StartResolveSequence();
        gameObject.SetActive(false);
    }
    
}
