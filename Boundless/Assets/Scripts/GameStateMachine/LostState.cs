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

	private IEnumerator LostMusicTransition() {
		yield return _gameStateMachine.BackgroundMusic.DOFade (0f, 10).WaitForCompletion();
		yield return new WaitForSeconds (2f);
		_gameStateMachine.SereneMusic.Play ();
	}

	private void StopDebris() {
		_gameStateMachine.DebrisParticleSystem.Stop();
		_gameStateMachine.DebrisParticleSystemForeground.Stop();
		_gameStateMachine.DebrisParticleSystemBackground.Stop();
		_gameStateMachine.DebrisParticleSystemRare.Stop();
	}

	private IEnumerator LostAnimationRoutine()
	{
		Debug.Log ("Lost");
		StopDebris ();
		var nautStartTrans = _gameStateMachine.Astronaut.transform;

		//tween nautStartTrans to 
//		var startPosition = _gameStateMachine.AstronautStartPosition;

		_gameStateMachine.StartCoroutine(LostMusicTransition());

		yield return nautStartTrans.DOMove (Vector2.zero, 3).WaitForCompletion();
		_gameStateMachine.Astronaut.InFreeFall = false;

		yield return new WaitForSeconds (16);

		float textFade = 1;
		//1
		SpriteRenderer text = GameObject.Instantiate (_gameStateMachine.Text1);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, textFade).WaitForCompletion();

		yield return new WaitForSeconds (5.5f);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, textFade).WaitForCompletion ();

		yield return new WaitForSeconds (2);


		//2
		text = GameObject.Instantiate (_gameStateMachine.Text2);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, textFade).WaitForCompletion();

		yield return new WaitForSeconds (4);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, textFade).WaitForCompletion ();

		yield return new WaitForSeconds (2);

		//3
		text = GameObject.Instantiate (_gameStateMachine.Text3);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, textFade).WaitForCompletion();

		yield return new WaitForSeconds (5.5f);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, textFade).WaitForCompletion ();

		yield return new WaitForSeconds (2);

		//4
		text = GameObject.Instantiate (_gameStateMachine.Text4);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, textFade).WaitForCompletion();

		yield return new WaitForSeconds (8);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, textFade).WaitForCompletion ();

		yield return new WaitForSeconds (2);

		//5
		text = GameObject.Instantiate (_gameStateMachine.Text5);
		text.color = new Color (1, 1, 1, 0);
		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 1, textFade).WaitForCompletion();

		yield return new WaitForSeconds (7);

		yield return DOTween.ToAlpha (() => text.color, x => text.color = x, 0, 5f).WaitForCompletion ();


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
