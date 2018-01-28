using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public AstronautBehaviour Astronaut;
    public BackgroundBehaviour BackgroundBehaviour;
    public Transform AstronautStartPosition;
    public GameplayController GameplayController;
    public MeshRenderer Fade;
	public AudioSource BackgroundMusic;
    
	public BeamBehaviour Beam;

    public List<Transform> SpawnPoints;
    
    public GameState State;

    public ItemFactory ItemFactory;
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