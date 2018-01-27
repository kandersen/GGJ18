using System.Collections;
using UnityEngine;

public class ChaseAndGetItemState : GameState
{
    private ItemBehaviour _target;
    private Coroutine _coroutine;

    public ChaseAndGetItemState(ItemBehaviour item, GameStateMachine gameStateMachine) :
        base(gameStateMachine)
    {
        _target = item;
    }

    public override void UpdateState()
    {
    }

    public override void EnterState()
    {
        _coroutine = _gameStateMachine.StartCoroutine(ChaseRoutine());
    }

    private IEnumerator ChaseRoutine()
    {
        var delta = _target.transform.position - _gameStateMachine.Alien.transform.position;
        while (delta.magnitude > 0.05f)
        {
            _gameStateMachine.Alien.transform.Translate(delta.normalized * Time.deltaTime * 6.0f);
            Debug.DrawLine(_gameStateMachine.Alien.transform.position, _target.transform.position, Color.cyan);
            yield return null;
            delta = _target.transform.position - _gameStateMachine.Alien.transform.position;
        }      
        _gameStateMachine.Alien.PickUpItem(_target);
        _target.State = ItemBehaviour.ItemState.Held;
        _target.transform.parent = _gameStateMachine.Alien.LeftHand;
        _target.transform.localPosition = Vector3.zero;
        _nextState = new FreeFallingState(_gameStateMachine);
    }

    public override void ExitState()
    {
    }

    public override void AlienReachedBottom()
    {
        _gameStateMachine.StopCoroutine(_coroutine);
        _nextState = new TransitionToNextStageState(_gameStateMachine);
    }

    public override void ItemClicked(ItemBehaviour item)
    {
        if (item != _target)
        {
            _gameStateMachine.StopCoroutine(_coroutine);
            _nextState = new ChaseAndGetItemState(item, _gameStateMachine);
        }
    }

    public override void ItemDriftedOff(ItemBehaviour item)
    {
        if (item == _target)
        {
            _gameStateMachine.StopCoroutine(_coroutine);
            _nextState = new FreeFallingState(_gameStateMachine);
        }
    }

    public override void BottomScreenPressed()
    {
        _gameStateMachine.StopCoroutine(_coroutine);
        _nextState = new TransitionToNextStageState(_gameStateMachine);
    }

    public override void PositionInSpacePressed(Vector2 pos)
    {
        _gameStateMachine.StopCoroutine(_coroutine);
        _nextState = new MovingState(pos, _gameStateMachine);    
    }
}