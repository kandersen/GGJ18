using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Signal MoveDownwardsSignal = new Signal();
    public Signal<ItemBehaviour> PickItemSignal = new Signal<ItemBehaviour>();
    public Signal<Vector2> MoveSignal = new Signal<Vector2>();
    public Signal<ItemBehaviour> ItemDriftedOffScreenSignal = new Signal<ItemBehaviour>();
    public Signal ActivateAstronautSignal = new Signal();    
    public Signal TransmitterReadySignal = new Signal();

    public GameStateMachine GameStateMachine;

    public void Start()
    {
        ConnectSignals();
    }
    
    public void ConnectSignals()
    {
        MoveDownwardsSignal.AddListener(HandleMoveDown);
        PickItemSignal.AddListener(HandlePickItem);
        MoveSignal.AddListener(HandleMove);
        ItemDriftedOffScreenSignal.AddListener(HandleItemDriftedOffScreen);
        ActivateAstronautSignal.AddListener(HandleActivateAstronaut);
        TransmitterReadySignal.AddListener(HandleTransmitterReady);
    }

    private void HandleActivateAstronaut()
    {
        GameStateMachine.State.AstronautActivated();
    }

    private void HandleItemDriftedOffScreen(ItemBehaviour item)
    {
        GameStateMachine.State.ItemDriftedOff(item);
    }

    private void HandleMove(Vector2 destination)
    {
        GameStateMachine.State.PositionInSpacePressed(destination);
    }

    private void HandlePickItem(ItemBehaviour item)
    {
        GameStateMachine.State.ItemClicked(item);
    }

    private void HandleMoveDown()
    {
        GameStateMachine.State.BottomScreenPressed();
    }

    private void HandleTransmitterReady()
    {
        GameStateMachine.State.TransmitterReady();
    }

    /*
	void Update() {
		if (Input.anyKey) {
			GameStateMachine.State.AnyKeyPressed ();
		}
	}*/
    
}