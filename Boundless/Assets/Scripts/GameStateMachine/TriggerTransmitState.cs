using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerTransmitState : GameState
{
	private GameState nextState;
	private Coroutine _coroutine;

	private BaseItem.CombinationResult result;

	public TriggerTransmitState( GameStateMachine gameStateMachine) : base(gameStateMachine)
	{
		ItemBehaviour item = _gameStateMachine.Astronaut.GetLeftItem ();
		if (item.BaseItem != null) {
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
		_coroutine = _gameStateMachine.StartCoroutine(TransmitRoutine());
	}

	private IEnumerator TransmitRoutine()
	{

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