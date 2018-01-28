using System.Collections;
using UnityEngine;

public class WinState : GameState
{
    public WinState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void UpdateState()
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

    public override void ExitState()
    {
    }

    public override void AlienReachedBottom()
    {
    }

    public override void ItemClicked(ItemBehaviour item)
    {
    }

    public override void ItemDriftedOff(ItemBehaviour item)
    {
    }

    public override void BottomScreenPressed()
    {
    }

    public override void PositionInSpacePressed(Vector2 pos)
    {
    }

    public override void AstronautActivated()
    {
    }
}
