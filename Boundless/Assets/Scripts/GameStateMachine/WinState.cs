using System.Collections;
using UnityEngine;
using DG.Tweening;

using UnityEngine.SceneManagement;

public class WinState : GameState
{
    public WinState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void EnterState()
    {
        _gameStateMachine.StartCoroutine(WinAnimationRoutine());
    }

	private IEnumerator FlashBeam(BeamBehaviour beam) {

		yield return null;
	}

	private IEnumerator DropItems() {
		yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < 2; i++) {
			if (_gameStateMachine.Astronaut.Items.Count > 0) {
				ItemBehaviour Transmitter = _gameStateMachine.Astronaut.Items.Pop ();
				Transmitter.State = ItemBehaviour.ItemState.Dropped;
				Transmitter.transform.parent = null;
				Transmitter.DriftBehaviour.enabled = true;
				_gameStateMachine.GameplayController.GameStateMachine.ActiveItems.Add (Transmitter);
			}
		}
	}

    private IEnumerator WinAnimationRoutine()
    {
		_gameStateMachine.Astronaut.InFreeFall = false;
		var nautStartTrans = _gameStateMachine.Astronaut.transform;

        //tween nautStartTrans to 
//        var startPosition = _gameStateMachine.AstronautStartPosition;

		yield return _gameStateMachine.AudioController.FadeBackgroundMusic (3);
		yield return nautStartTrans.DOMove (Vector2.zero, 3).WaitForCompletion();

		BeamBehaviour beam = GameObject.Instantiate (_gameStateMachine.Beam);
		beam.gameObject.transform.position = new Vector2 (nautStartTrans.position.x+0.5f, beam.gameObject.transform.position.y);
		beam.BeamRenderer.color = new Color(1,1,1,0);
		beam.LineRenderer.color = new Color(1,1,1,0);
		DOTween.ToAlpha (() => beam.BeamRenderer.color, x => beam.BeamRenderer.color = x, 0.5f, 0.2f);
		yield return DOTween.ToAlpha (() => beam.LineRenderer.color, x => beam.LineRenderer.color = x, 1f, 0.2f).WaitForCompletion();
		_gameStateMachine.StartCoroutine (FlashBeam(beam));

//		_gameStateMachine.StartCoroutine (DropItem ());

		yield return nautStartTrans.DOMove (new Vector2(nautStartTrans.position.x,10f),4f).WaitForCompletion();

		PersistentData.GameStarted = true;
		SceneManager.LoadSceneAsync ("Intro");
    }
}
