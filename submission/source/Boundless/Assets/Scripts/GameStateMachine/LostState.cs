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

		yield return new WaitForSeconds (10);


		//1
		SpriteRenderer text = GameObject.Instantiate (_gameStateMachine.Text1);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, 0.5f).WaitForCompletion();

		yield return new WaitForSeconds (7);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, 0.5f).WaitForCompletion ();

		yield return new WaitForSeconds (3);


		//2
		text = GameObject.Instantiate (_gameStateMachine.Text2);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, 0.5f).WaitForCompletion();

		yield return new WaitForSeconds (5);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, 0.5f).WaitForCompletion ();

		yield return new WaitForSeconds (3);

		//3
		text = GameObject.Instantiate (_gameStateMachine.Text3);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, 0.5f).WaitForCompletion();

		yield return new WaitForSeconds (7);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, 0.5f).WaitForCompletion ();

		yield return new WaitForSeconds (3);

		//4
		text = GameObject.Instantiate (_gameStateMachine.Text4);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, 0.5f).WaitForCompletion();

		yield return new WaitForSeconds (10);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, 0.5f).WaitForCompletion ();

		yield return new WaitForSeconds (3);

		//5
		text = GameObject.Instantiate (_gameStateMachine.Text5);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, 0.5f).WaitForCompletion();

		yield return new WaitForSeconds (7);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, 0.5f).WaitForCompletion ();

		yield return new WaitForSeconds (3);

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
