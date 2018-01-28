using System.Collections;
using UnityEngine;

public class MovingState : GameState
{
    private readonly Vector3 _target;
    private Coroutine _coroutine;

    public MovingState(Vector2 pos, GameStateMachine gameStateMachine) :
        base(gameStateMachine)
    {
        _target = pos;
    }

    public override void UpdateState()
    {
    }

    public override void EnterState()
    {
        _coroutine = _gameStateMachine.StartCoroutine(MoveRoutine());
        _gameStateMachine.Astronaut.JetPackSound.Play();
    }

    private IEnumerator MoveRoutine()
    {
        var delta = _target - _gameStateMachine.Astronaut.transform.position;
        var speed = delta.y > 0 ? 4.0f : 8.0f;
        while (delta.magnitude > 0.1f)
        {          
            _gameStateMachine.Astronaut.transform.Translate(delta.normalized * Time.deltaTime * speed);
            Debug.DrawLine(_gameStateMachine.Astronaut.transform.position, _target, Color.green);
            yield return null;
            delta = _target - _gameStateMachine.Astronaut.transform.position;
        }
        _gameStateMachine.Astronaut.JetPackSound.Stop();
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
        _gameStateMachine.StopCoroutine(_coroutine);
        _nextState = new ChaseAndGetItemState(item, _gameStateMachine);
    }

    public override void ItemDriftedOff(ItemBehaviour item)
    {        
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

    public override void AstronautActivated()
    {
        _gameStateMachine.Astronaut.Activate();
    }
}