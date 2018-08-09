using UnityEngine;

public abstract class GameState
{
    protected GameStateMachine _gameStateMachine;
    protected GameState _nextState;    

    protected GameState(GameStateMachine gameStateMachine)
    {
        _nextState = null;
        _gameStateMachine = gameStateMachine;
    }

    public virtual void UpdateState()
    {
    }

    public virtual void EnterState()
    {
    }

    public virtual void ExitState()
    {
        
    }

    public GameState GetNext()
    {
        return _nextState;
    }

    public virtual void AlienReachedBottom()
    {
    }

	public virtual void StopChase()
	{
	}

    public virtual void ItemClicked(ItemBehaviour item)
    {
    }

    public virtual void ItemDriftedOff(ItemBehaviour item)
    {
    }

    public virtual void BottomScreenPressed()
    {
    }

    public virtual void PositionInSpacePressed(Vector2 pos)
    {
    }

    public virtual void AstronautActivated()
    {
    }

    public virtual void TransmitterReady()
    {
    }

	public virtual void AnyKeyPressed() 
    {        
    }
}