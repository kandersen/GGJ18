﻿using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TransitionToNextStageState : GameState
{
    public TransitionToNextStageState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void EnterState()
    {
        if (_gameStateMachine.RoundsToGo < 0)
        {
            
        }
        _gameStateMachine.StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {                
        var alien = _gameStateMachine.Astronaut;
        var background = _gameStateMachine.BackgroundBehaviour;
        alien.InFreeFall = false;
        
        DOTween.To(() => background.speed, x => background.speed = x, 2.0f, 0.5f);
        foreach (var item in _gameStateMachine.ActiveItems)
        {
            item.Velocity = 8.0f;
        }        
        _gameStateMachine.Astronaut.JetPackSound.Play();
        yield return alien.transform.DOMove(_gameStateMachine.AstronautStartPosition.position, 3.0f).WaitForCompletion();
        _gameStateMachine.Astronaut.JetPackSound.Stop();
        DOTween.To(() => background.speed, x => background.speed = x, 0.2f, 0.5f);
        
        alien.InFreeFall = true;

        foreach (var item in _gameStateMachine.ActiveItems)
        {
            item.gameObject.SetActive( false );
        }
        _gameStateMachine.ActiveItems.Clear();

        for (var i = 0; i < _gameStateMachine.RoundsToGo; i++)
        {
            var item = _gameStateMachine.ItemFactory.SpawnItem();
            item.transform.position = new Vector3(Random.value * 14f - 7.0f, -8.0f, 0f);
            _gameStateMachine.ActiveItems.Add(item);
        }

        _gameStateMachine.RoundsToGo--;
        var emissions = _gameStateMachine.DebrisParticleSystem.emission;
        emissions.rateOverTime = new ParticleSystem.MinMaxCurve(emissions.rateOverTime.constant - 1.0f);
                       
        _nextState = new FreeFallingState(_gameStateMachine);
    }
}