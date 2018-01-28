using System.Collections;
using UnityEngine;
using DG.Tweening;

using UnityEngine.SceneManagement;

public class LostState : GameState
{
	public LostState(GameStateMachine gameStateMachine) : base(gameStateMachine)
	{
	}

	public override void UpdateState()
	{
	}

	public override void EnterState()
	{
		_gameStateMachine.StartCoroutine(LostAnimationRoutine());
	}

	private IEnumerator LostAnimationRoutine()
	{
		Debug.Log ("Lost");
		var nautStartTrans = _gameStateMachine.Astronaut.transform;

		//tween nautStartTrans to 
		var startPosition = _gameStateMachine.AstronautStartPosition;

		_gameStateMachine.BackgroundMusic.DOFade (0.2f, 3);
		yield return nautStartTrans.DOMove (Vector2.zero, 3).WaitForCompletion();

		yield return new WaitForSeconds (15);

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
	}

	public override void PositionInSpacePressed(Vector2 pos)
	{
	}

	public override void AstronautActivated()
	{
	}
}
