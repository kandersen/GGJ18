using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public AstronautBehaviour Astronaut;
    public BackgroundBehaviour BackgroundBehaviour;
    public Transform AstronautStartPosition;
    public GameplayController GameplayController;
    public MeshRenderer Fade;


    public DebrisParticleSystems DebrisParticleSystem;
	public DebrisParticleSystems DebrisParticleSystemForeground;
	public DebrisParticleSystems DebrisParticleSystemBackground;
	public DebrisParticleSystems DebrisParticleSystemRare;
    
	public BeamBehaviour Beam;
	public SpriteRenderer Text1;
	public SpriteRenderer Text2;
	public SpriteRenderer Text3;
	public SpriteRenderer Text4;
	public SpriteRenderer Text5;

	public AudioController AudioController;

    public List<Transform> SpawnPoints;
    
    public GameState State;

    public ItemFactory ItemFactory;
    public List<ItemBehaviour> DiscardedItems = new List<ItemBehaviour>();
    public List<ItemBehaviour> ActiveItems = new List<ItemBehaviour>();

	public Animator AstroAnimation;
    
    public int RoundsToGo { get; set; }

    public void Start()
    {
        RoundsToGo = 7;
        State = new GameplayIntroState(this);        
        State.EnterState();        
    }
    
    public void Update()
    {
        if (State.GetNext() != null)
        {          
            State.ExitState();
            State = State.GetNext();
            State.EnterState();
        }
        State.UpdateState();
    }

}