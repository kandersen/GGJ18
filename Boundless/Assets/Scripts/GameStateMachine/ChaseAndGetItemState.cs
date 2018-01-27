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
        var delta = _target.transform.position - _gameStateMachine.Astronaut.transform.position;
        while (delta.magnitude > 0.05f)
        {
            _gameStateMachine.Astronaut.transform.Translate(delta.normalized * Time.deltaTime * 6.0f);
            Debug.DrawLine(_gameStateMachine.Astronaut.transform.position, _target.transform.position, Color.cyan);
            yield return null;
            delta = _target.transform.position - _gameStateMachine.Astronaut.transform.position;
        }      
        var droppedItem = _gameStateMachine.Astronaut.PickUpItem(_target);
        if (droppedItem != null)
        {
            _gameStateMachine.ActiveItems.Add(droppedItem);
        }
        _gameStateMachine.ActiveItems.Remove(_target);
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