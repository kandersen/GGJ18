using UnityEngine;

public abstract class GameState
{
    public abstract void UpdateState();
    public abstract void EnterState();
    public abstract void ExitState();

    protected GameStateMachine _gameStateMachine;
    protected GameState _nextState;    

    protected GameState(GameStateMachine gameStateMachine)
    {
        _nextState = null;
        _gameStateMachine = gameStateMachine;
    }

    public GameState GetNext()
    {
        return _nextState;
    }

    public abstract void AlienReachedBottom();
    public abstract void ItemClicked(ItemBehaviour item);
    public abstract void ItemDriftedOff(ItemBehaviour item);
    public abstract void BottomScreenPressed();
    public abstract void PositionInSpacePressed(Vector2 pos);
    public abstract void AstronautActivated();
	public virtual void AnyKeyPressed() {}
}