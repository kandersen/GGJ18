using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameplayIntroState : GameState
{
    public GameplayIntroState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void EnterState()
    {
        _gameStateMachine.Fade.gameObject.SetActive( true );
        _gameStateMachine.Fade.material.color = Color.black;
        _gameStateMachine.BackgroundBehaviour.speed = 10f;
        _gameStateMachine.Astronaut.transform.position = Vector3.up * 10;
        _gameStateMachine.StartCoroutine(EnterAstronautRoutine());
    }

    private IEnumerator EnterAstronautRoutine()
    {
        var naut = _gameStateMachine.Astronaut.transform;
        yield return new WaitForSeconds(2.0f);
        DOTween.To(() => _gameStateMachine.BackgroundBehaviour.speed,
            x => _gameStateMachine.BackgroundBehaviour.speed = x, 0.2f, 3.0f).SetEase(Ease.OutCirc);
        DOTween.ToAlpha(() => _gameStateMachine.Fade.material.color, c => _gameStateMachine.Fade.material.color = c,
            0.0f, 0.5f);
        yield return naut.DOMove(_gameStateMachine.AstronautStartPosition.position, 3.0f).SetEase(Ease.OutElastic).WaitForCompletion();
        _gameStateMachine.Fade.gameObject.SetActive( false );
        _nextState = new FreeFallingState(_gameStateMachine);
    }
}