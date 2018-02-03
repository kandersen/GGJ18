using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeFallingState : GameState
{        
    
    public FreeFallingState(GameStateMachine gameStateMachine) : 
        base(gameStateMachine)
    {
    }

    public override void EnterState()
    {
        _gameStateMachine.StartCoroutine(ReleaseRoutine());
    }

    private IEnumerator ReleaseRoutine()
    {
        yield return _gameStateMachine.Astronaut.transform.DOPunchPosition(Vector3.up * 0.1f, 0.4f, elasticity:0f).SetEase(Ease.OutSine).WaitForCompletion();
        _gameStateMachine.Astronaut.InFreeFall = true;
    }

    public override void ExitState()
    {
        _gameStateMachine.Astronaut.InFreeFall = false;
    }

    public override void AlienReachedBottom()
    {
        _nextState = new TransitionToNextStageState(_gameStateMachine);
    }

    public override void ItemClicked(ItemBehaviour item)
    {
        _nextState = new ChaseAndGetItemState(item, _gameStateMachine);
    }
    
    public override void BottomScreenPressed()
    {
        _nextState = new TransitionToNextStageState(_gameStateMachine);
    }

    public override void PositionInSpacePressed(Vector2 pos)
    {
        _nextState = new MovingState(pos, _gameStateMachine);
    }

    public override void AstronautActivated()
    {
        _gameStateMachine.Astronaut.Activate();
    }
		
    public override void TransmitterReady()
    {
        _nextState = new WinState(_gameStateMachine);
    }

    public override void AnyKeyPressed()
    {
		if(Input.GetKeyDown("w")) {
			Debug.Log ("Win start");
        	_nextState = new WinState (_gameStateMachine);
		}
		if(Input.GetKeyDown("l")) {
			Debug.Log ("Lost start");
			_nextState = new LostState (_gameStateMachine);
		}
		if(Input.GetKeyDown("t")) {
			Debug.Log ("Transmit");
			_nextState = new TriggerTransmitState (_gameStateMachine);
		}
		if (Input.GetKeyDown ("r")) {
			PersistentData.GameStarted = false;
			SceneManager.LoadSceneAsync ("Intro");
		}
    }

}