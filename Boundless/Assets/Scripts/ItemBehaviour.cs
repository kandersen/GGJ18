using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public enum ItemState
    {
        Drifting = 0,
        Held = 1,
        Dropped = 2,
    }

    public BaseItem BaseItem;
    public AddOn AddOn;
    
    public GameplayController GameplayController;
    public AstronautAudio AudioSource;
    public DriftBehaviour DriftBehaviour;
    public ItemState State = ItemState.Drifting;
    public float Velocity = 2.0f;

    public ColliderEventReporter ColliderEventReport;

    public void Start()
    {
        ColliderEventReport.OnClickedSignal.AddListener(HandleOnClick);
    }

    private void HandleOnClick()
    {
        if (State == ItemState.Drifting)
        {
            GameplayController.PickItemSignal.Dispatch(this);            
        }
    }

    public void Update()
    {
        if (State == ItemState.Held) return;
        
        Vector2 position = transform.position;
        position += Vector2.up * Time.deltaTime * Velocity;
        transform.position = position;
    }

    public List<ItemBehaviour> Combine(ItemBehaviour other)
    {
        if (BaseItem == null && other.BaseItem == null)
        {
            AudioSource.PlayCombineFailure();            ;
            return new List<ItemBehaviour>() {this, other};                    
        }

        if (AddOn != null && other.AddOn != null)
        {
            AudioSource.PlayCombineFailure();
            return new List<ItemBehaviour>() {this, other};            
        }

        var combineBase = this;
        var newPiece = other;
        if (BaseItem == null)
        {
            newPiece = this;
            combineBase = other;
        }

        var attemptResult = combineBase.BaseItem.Attach(newPiece.AddOn);
        if (attemptResult == null)
        {
            AudioSource.PlayCombineSuccess();
            return new List<ItemBehaviour>() {combineBase};
        }
        else
        {
            AudioSource.PlayCombineFailure();
            return new List<ItemBehaviour>() {combineBase, attemptResult.Item};
        }
    }
}