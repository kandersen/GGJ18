﻿using System.Collections;
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

    public override void EnterState()
    {
		_gameStateMachine.AudioController.PlayJetPackSound();
        _coroutine = _gameStateMachine.StartCoroutine(ChaseRoutine());
    }

    private IEnumerator ChaseRoutine()
    {
        var delta = _target.transform.position - _gameStateMachine.Astronaut.transform.position;      
        while (delta.magnitude > 0.1f)
        {
            _gameStateMachine.Astronaut.transform.Translate(delta.normalized * Time.deltaTime * 6.0f);
            Debug.DrawLine(_gameStateMachine.Astronaut.transform.position, _target.transform.position, Color.cyan);
            yield return null;
            delta = _target.transform.position - _gameStateMachine.Astronaut.transform.position;
        }      
		_gameStateMachine.AudioController.StopJetPackSound ();    
        var droppedItem = _gameStateMachine.Astronaut.PickUpItem(_target);
		_gameStateMachine.AudioController.PlayPickupBattery();
        if (droppedItem != null)
        {
            _gameStateMachine.ActiveItems.Add(droppedItem);
        }
        _gameStateMachine.ActiveItems.Remove(_target);
        _nextState = new FreeFallingState(_gameStateMachine);
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

	public override void StopChase() {
		Debug.Log ("Stop chase");
		_gameStateMachine.StopCoroutine(_coroutine);
		_nextState = new FreeFallingState(_gameStateMachine);
	}

    public override void ItemDriftedOff(ItemBehaviour item)
    {
        if (item == _target)
        {
			Debug.Log ("Item drifted off");
			StopChase ();
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

    public override void AstronautActivated()
    {
        _gameStateMachine.Astronaut.Activate();
    }
    
    public override void TransmitterReady()
    {
		_nextState = new TriggerTransmitState(_gameStateMachine);
    }

}