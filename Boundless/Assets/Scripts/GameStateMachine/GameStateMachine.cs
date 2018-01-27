using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public AstronautBehaviour Astronaut;
    public BackgroundBehaviour BackgroundBehaviour;
    public Transform AstronautStartPosition;
    public GameplayController GameplayController;

    public List<Transform> SpawnPoints;
    
    public GameState State;

    public ItemBehaviour BatteryPrefab;
    public List<ItemBehaviour> DiscardedItems = new List<ItemBehaviour>();
    public List<ItemBehaviour> ActiveItems = new List<ItemBehaviour>();

    public void Start()
    {
        State = new GameplayIntroState(this);
        State.EnterState();
    }
    
    public void Update()
    {
        if (State.GetNext() != null)
        {
            State = State.GetNext();
            State.EnterState();
        }
        State.UpdateState();
    }   
}

public class GameplayIntroState : GameState
{
    public GameplayIntroState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void UpdateState()
    {
    }

    public override void EnterState()
    {
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
        yield return naut.DOMove(_gameStateMachine.AstronautStartPosition.position, 3.0f).SetEase(Ease.OutElastic).WaitForCompletion();
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
}