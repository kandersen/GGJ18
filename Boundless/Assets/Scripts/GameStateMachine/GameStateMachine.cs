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
<<<<<<< HEAD
        {
			State.ExitState ();
=======
        {            
>>>>>>> 24ec9f40ade5b73aa7d7f8ded4c2c4b7ac165e6f
            State = State.GetNext();
            State.EnterState();
        }
        State.UpdateState();
    }

}