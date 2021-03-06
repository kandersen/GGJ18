﻿using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TriggerTransmitState : GameState
{
	private GameState nextState;
	private Coroutine _coroutine;
	private Coroutine _moveCoroutine;

	private BaseItem.CombinationResult result;

	public TriggerTransmitState( GameStateMachine gameStateMachine) : base(gameStateMachine)
	{
		ItemBehaviour item = _gameStateMachine.Astronaut.GetLeftItem ();
		if (item != null && item.BaseItem != null) {
			result = item.BaseItem.CheckCompletion ();
		} else {
			result = BaseItem.CombinationResult.NotDone;
		}
		nextState = new FreeFallingState (_gameStateMachine);
	}

	public override void UpdateState()
	{
	}

	public override void EnterState()
	{
		if (result != BaseItem.CombinationResult.NotDone) {
			_coroutine = _gameStateMachine.StartCoroutine (TransmitRoutine ());
		} else {
			_nextState = nextState;
		}
	}

	private IEnumerator TransmitRoutine()
	{
		_moveCoroutine = _gameStateMachine.StartCoroutine (MoveAstronaut ());
		_gameStateMachine.AstroAnimation.enabled = false;
		var astronaut = _gameStateMachine.Astronaut;
		if (astronaut.Items.Count > 1) {
			var item = astronaut.Items.Pop ();
			item.transform.parent = null;					
			item.State = ItemBehaviour.ItemState.Dropped;
		}
		if (result == BaseItem.CombinationResult.Dud) {

			yield return new WaitForSeconds (0.2f);
			for (var i = 0; i < 3; i++) {
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.1f).WaitForCompletion ();
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.1f).WaitForCompletion ();
			}
			yield return new WaitForSeconds (0.5f);
			for (var i = 0; i < 3; i++) {
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.1f).WaitForCompletion ();
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.1f).WaitForCompletion ();
			}
			yield return new WaitForSeconds (0.3f);
			var item = astronaut.Items.Pop ();
			item.transform.parent = null;					
			item.State = ItemBehaviour.ItemState.Dropped;

		} else {
			astronaut.RightArmRenderer.flipX = false;
			if (result == BaseItem.CombinationResult.Switchable) {
				yield return new WaitForSeconds (0.3f);
				for (var i = 0; i < 2; i++) {
					_gameStateMachine.AudioController.PlayFlickSwitch ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.2f).WaitForCompletion ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.2f).WaitForCompletion ();
				}
				yield return new WaitForSeconds (0.3f);
				for (var i = 0; i < 3; i++) {
					yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.15f).WaitForCompletion ();
					yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.15f).WaitForCompletion ();
				}
				yield return new WaitForSeconds (0.3f);
				for (var i = 0; i < 2; i++) {
					_gameStateMachine.AudioController.PlayFlickSwitch ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.2f).WaitForCompletion ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.2f).WaitForCompletion ();
				}
				yield return new WaitForSeconds (0.3f);
				astronaut.RightArmRenderer.flipX = true;

				var item = astronaut.Items.Pop ();
				item.transform.parent = null;					
				item.State = ItemBehaviour.ItemState.Dropped;
			} else if (result == BaseItem.CombinationResult.Success) {
				yield return new WaitForSeconds (0.3f);
				_gameStateMachine.AudioController.PlayFlickSwitch ();
				yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.2f).WaitForCompletion ();
				yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.2f).WaitForCompletion ();
				_gameStateMachine.StartCoroutine (AntennaTransmit());
				yield return new WaitForSeconds (0.15f);
				_gameStateMachine.AudioController.PlayTransmit ();
				yield return new WaitForSeconds (0.3f);
				astronaut.RightArmRenderer.flipX = true;
				//TODO add animation for transmission
				nextState = new WinState (_gameStateMachine);
			}

		}
		_gameStateMachine.AstroAnimation.enabled = true;
		_nextState = nextState;
		yield return null;
	}

	private IEnumerator AntennaTransmit() {
		_gameStateMachine.Astronaut.GetLeftItem ().BaseItem.first.AddOn.Transmit ();
		_gameStateMachine.Astronaut.GetLeftItem ().BaseItem.second.AddOn.Transmit ();
		yield return new WaitForSeconds (3f);
		_gameStateMachine.Astronaut.GetLeftItem ().BaseItem.first.AddOn.TransmitStop ();
		_gameStateMachine.Astronaut.GetLeftItem ().BaseItem.second.AddOn.TransmitStop ();
	}

	private IEnumerator MoveAstronaut() {
		while(true) {
			Vector2 position = _gameStateMachine.Astronaut.transform.position;
			position += Vector2.down * Time.deltaTime * 0.5f;
			_gameStateMachine.Astronaut.transform.position = position;
			yield return new WaitForFixedUpdate();
		}
	}

	public override void ExitState()
	{
		_gameStateMachine.StopCoroutine (_moveCoroutine);
	}

	public override void BottomScreenPressed()
	{
		nextState = new TransitionToNextStageState (_gameStateMachine);
	}

}