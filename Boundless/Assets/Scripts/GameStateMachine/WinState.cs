using System.Collections;

public class WinState : GameState
{
    public WinState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void EnterState()
    {
        _gameStateMachine.StartCoroutine(WinAnimationRoutine());
    }

    private IEnumerator WinAnimationRoutine()
    {
        var nautStartTrans = _gameStateMachine.Astronaut.transform;
        //tween nautStartTrans to 
        var startPosition = _gameStateMachine.AstronautStartPosition;
        yield return null;
    }
}
