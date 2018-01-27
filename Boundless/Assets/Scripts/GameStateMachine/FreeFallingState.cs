using UnityEngine;

public class FreeFallingState : GameState
{        
    
    public FreeFallingState(GameStateMachine gameStateMachine) : 
        base(gameStateMachine)
    {
    }

    public override void UpdateState()
    {
    }

    public override void EnterState()
    {
        _gameStateMachine.Alien.InFreeFall = true;
    }

    public override void ExitState()
    {
        _gameStateMachine.Alien.InFreeFall = false;
    }

    public override void AlienReachedBottom()
    {
        _nextState = new TransitionToNextStageState(_gameStateMachine);
    }

    public override void ItemClicked(ItemBehaviour item)
    {
        _nextState = new ChaseAndGetItemState(item, _gameStateMachine);
    }

    public override void ItemDriftedOff(ItemBehaviour item)
    {
        return;
    }

    public override void BottomScreenPressed()
    {
        _nextState = new TransitionToNextStageState(_gameStateMachine);
    }

    public override void PositionInSpacePressed(Vector2 pos)
    {
        _nextState = new MovingState(pos, _gameStateMachine);
    }

}