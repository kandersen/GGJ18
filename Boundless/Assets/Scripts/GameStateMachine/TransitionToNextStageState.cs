using System.Collections;
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
            _nextState = new LostState(_gameStateMachine);
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
            item.Velocity += 7.0f;
        }        
		_gameStateMachine.AstroAnimation.SetBool ("fastFalling", true);
        _gameStateMachine.Astronaut.JetPackSound.Play();
        yield return alien.transform.DOMove(_gameStateMachine.AstronautStartPosition.position, 3.0f).WaitForCompletion();
        _gameStateMachine.Astronaut.JetPackSound.Stop();
		_gameStateMachine.AstroAnimation.SetBool ("fastFalling", false);
        DOTween.To(() => background.speed, x => background.speed = x, 0.2f, 0.5f);
        
        foreach (var item in _gameStateMachine.ActiveItems)
        {
            item.gameObject.SetActive( false );
        }
        _gameStateMachine.ActiveItems.Clear();

        for (var i = 0; i < _gameStateMachine.RoundsToGo; i++)
        {
			
            var item = _gameStateMachine.ItemFactory.SpawnItem();
			item.transform.position = new Vector3(Random.value * 20f - 10.0f, -8.0f - 3 * Random.value, 0f);
			Debug.Log (item.Velocity);
			item.Velocity = 2 + 2f * Random.value;
            _gameStateMachine.ActiveItems.Add(item);
        }

        _gameStateMachine.RoundsToGo--;
		_gameStateMachine.DebrisParticleSystemBackground.DecrementSpawnRate (1.0f / 7.0f);
		_gameStateMachine.DebrisParticleSystem.DecrementSpawnRate(1.0f/6.0f);
		_gameStateMachine.DebrisParticleSystemForeground.DecrementSpawnRate (0.1f/5.0f);
		_gameStateMachine.DebrisParticleSystemRare.DecrementSpawnRate (0.23f/6.0f);

         
        _nextState = new FreeFallingState(_gameStateMachine);
    }
}