using UnityEngine;

public class ChaseAndGetItemState : GameState
{
    private ItemBehaviour _target;
    private Transform _alienTransform;
    
    public ChaseAndGetItemState(ItemBehaviour item, GameStateMachine gameStateMachine) :
        base(gameStateMachine)
    {
        _target = item;
        _alienTransform = _gameStateMachine.Alien.transform;
    }

    public override void UpdateState()
    {
        var delta = _target.transform.position - _alienTransform.position;
        if (delta.magnitude > 0.05f)
        {
            // move towards item
        }
        else
        {
            // pickup item
            _nextState = new FreeFallingState(_gameStateMachine);
        }
    }

    public override void EnterState()
    {        
    }

    public override void ExitState()
    {
    }

    public override void AlienReachedBottom()
    {
        _nextState = new TransitionToNextStageState(_gameStateMachine);
    }

    public override void ItemClicked(ItemBehaviour item)
    {
        if (item != _target)
        {
            _nextState = new ChaseAndGetItemState(item, _gameStateMachine);
        }
    }

    public override void ItemDriftedOff(ItemBehaviour item)
    {
        if (item == _target)
        {
            _nextState = new FreeFallingState(_gameStateMachine);
        }
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