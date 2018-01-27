using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TransitionToNextStageState : GameState
{
    public TransitionToNextStageState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void UpdateState()
    {        
    }

    public override void EnterState()
    {
        _gameStateMachine.StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        var alien = _gameStateMachine.Alien;
        var background = _gameStateMachine.BackgroundBehaviour;
        alien.InFreeFall = false;
        DOTween.To(()=> background.speed, x=> background.speed = x, 2.0f, 0.5f);
        yield return alien.transform.DOMove(_gameStateMachine.AlienStartPosition.position, 3.0f).WaitForCompletion();
        DOTween.To(()=> background.speed, x=> background.speed = x, 0.2f, 0.5f);
        alien.InFreeFall = true;
        _nextState = new FreeFallingState(_gameStateMachine);
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
}