using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TransitionToNextStageState : GameState
{
    public TransitionToNextStageState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void UpdateState()
    {        
    }

    public override void EnterState()
    {
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
            
        foreach (var spawnpoint in _gameStateMachine.SpawnPoints)
        {
            var item = _gameStateMachine.ItemFactory.SpawnItem();
            item.transform.position = spawnpoint.position;
            _gameStateMachine.ActiveItems.Add(item);
        }
        
        _nextState = new FreeFallingState(_gameStateMachine);
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