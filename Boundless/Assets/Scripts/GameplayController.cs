using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Signal MoveDownwardsSignal = new Signal();
    public Signal<ItemBehaviour> PickItemSignal = new Signal<ItemBehaviour>();
    public Signal<Vector2> MoveSignal = new Signal<Vector2>();
    public Signal<ItemBehaviour> ItemDriftedOffScreenSignal = new Signal<ItemBehaviour>();

    public AlienBehaviour Alien;
    public BackgroundBehaviour Background;
    public Transform AlienStartPosition;

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
    }

    private void HandleItemDriftedOffScreen(ItemBehaviour obj)
    {
        
    }

    private void HandleMove(Vector2 destination)
    {
        GameStateMachine._state.PositionInSpacePressed(destination);
    }

    private void HandlePickItem(ItemBehaviour item)
    {
        GameStateMachine._state.ItemClicked(item);
    }

    private void HandleMoveDown()
    {
        GameStateMachine._state.BottomScreenPressed();
    }
}