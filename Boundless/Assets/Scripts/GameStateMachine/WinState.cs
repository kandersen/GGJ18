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

    private IEnumerator WinAnimationRoutine()
    {
		var nautStartTrans = _gameStateMachine.Astronaut.transform;

        //tween nautStartTrans to 
        var startPosition = _gameStateMachine.AstronautStartPosition;

		_gameStateMachine.BackgroundMusic.DOFade (0, 3);
		yield return nautStartTrans.DOMove (Vector2.zero, 3).WaitForCompletion();

		BeamBehaviour beam = GameObject.Instantiate (_gameStateMachine.Beam);
		beam.gameObject.transform.position = new Vector2 (nautStartTrans.position.x+0.5f, beam.gameObject.transform.position.y);
		beam.BeamRenderer.color = new Color (1, 1, 1, 0.5f);

		yield return nautStartTrans.DOMove (new Vector2(nautStartTrans.position.x,10f),4f).WaitForCompletion();

		PersistentData.GameStarted = true;
		SceneManager.LoadSceneAsync ("Intro");
    }
}
