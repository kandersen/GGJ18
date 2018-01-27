using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public AlienBehaviour Alien;
    public BackgroundBehaviour BackgroundBehaviour;
    public Transform AlienStartPosition;

    public GameState _state;

    public void Start()
    {
        _state = new FreeFallingState(this);
        _state.EnterState();
    }
    
    public void Update()
    {
        if (_state.GetNext() != null)
        {
            _state = _state.GetNext();
            _state.EnterState();
        }
        _state.UpdateState();
    }   
}