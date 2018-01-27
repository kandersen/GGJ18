using UnityEngine;

public class MovingState : GameState
{
    private readonly Vector3 _target;
    private float _speed;
    private Vector3 _direction;

    public MovingState(Vector2 pos, GameStateMachine gameStateMachine) :
        base(gameStateMachine)
    {
        _target = pos;
    }

    public override void UpdateState()
    {
        var delta = _target - _gameStateMachine.Alien.transform.position;
        if (delta.magnitude > 0.05f)
        {
            _gameStateMachine.Alien.transform.Translate(_direction * Time.deltaTime * _speed);
        }
        else
        {
            _nextState = new FreeFallingState(_gameStateMachine);
        }
        
    }

    public override void EnterState()
    {
        var delta = _target - _gameStateMachine.Alien.transform.position;
        _speed = delta.y > 0 ? 2.0f : 4.0f;
        _direction = delta.normalized;
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
        _nextState = new ChaseAndGetItemState(item, _gameStateMachine);
    }

    public override void ItemDriftedOff(ItemBehaviour item)
    {        
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