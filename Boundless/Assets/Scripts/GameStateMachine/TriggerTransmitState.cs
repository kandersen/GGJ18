using System.Collections;
using System.Collections.Generic;
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
		AstronautBehaviour astronaut = _gameStateMachine.Astronaut;
		if (astronaut.Items.Count > 1) {
			ItemBehaviour result = astronaut.Items.Pop ();
			result.transform.parent = null;					
			result.State = ItemBehaviour.ItemState.Dropped;
		}
		if (result == BaseItem.CombinationResult.Dud) {

			yield return new WaitForSeconds (0.2f);
			for (int i = 0; i < 3; i++) {
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.1f).WaitForCompletion ();
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.1f).WaitForCompletion ();
			}
			yield return new WaitForSeconds (0.5f);
			for (int i = 0; i < 3; i++) {
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.1f).WaitForCompletion ();
				yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.1f).WaitForCompletion ();
			}
			yield return new WaitForSeconds (0.3f);
			ItemBehaviour result = astronaut.Items.Pop ();
			result.transform.parent = null;					
			result.State = ItemBehaviour.ItemState.Dropped;

		} else {
			astronaut.RightArmRenderer.flipX = false;
			if (result == BaseItem.CombinationResult.Switchable) {
				yield return new WaitForSeconds (0.3f);
				for (int i = 0; i < 2; i++) {
					_gameStateMachine.AudioController.PlayFlickSwitch ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.2f).WaitForCompletion ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.2f).WaitForCompletion ();
				}
				yield return new WaitForSeconds (0.3f);
				for (int i = 0; i < 3; i++) {
					yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.15f).WaitForCompletion ();
					yield return _gameStateMachine.Astronaut.LeftArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.15f).WaitForCompletion ();
				}
				yield return new WaitForSeconds (0.3f);
				for (int i = 0; i < 2; i++) {
					_gameStateMachine.AudioController.PlayFlickSwitch ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.2f).WaitForCompletion ();
					yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.2f).WaitForCompletion ();
				}
				yield return new WaitForSeconds (0.3f);
				astronaut.RightArmRenderer.flipX = true;

				ItemBehaviour result = astronaut.Items.Pop ();
				result.transform.parent = null;					
				result.State = ItemBehaviour.ItemState.Dropped;
			} else if (result == BaseItem.CombinationResult.Success) {
				yield return new WaitForSeconds (0.3f);
				_gameStateMachine.AudioController.PlayFlickSwitch ();
				yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, 7),0.2f).WaitForCompletion ();
				yield return _gameStateMachine.Astronaut.RightArmRenderer.gameObject.transform.DOBlendableRotateBy (new Vector3 (0, 0, -7),0.2f).WaitForCompletion ();
				yield return new WaitForSeconds (0.3f);
				astronaut.RightArmRenderer.flipX = true;
				_gameStateMachine.AudioController.PlayTransmit ();
				//TODO add animation for transmission
				nextState = new WinState (_gameStateMachine);
			}

		}
		_gameStateMachine.AstroAnimation.enabled = true;
		_nextState = nextState;
		yield return null;
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
		nextState = new TransitionToNextStageState (_gameStateMachine);
	}

	public override void PositionInSpacePressed(Vector2 pos)
	{

	}

	public override void AstronautActivated()
	{
		
	}

	public override void TransmitterReady()
	{

	}

}