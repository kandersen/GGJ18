﻿using System.Collections.Generic;
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
    public DriftBehaviour DriftBehaviour;
    public ItemState State = ItemState.Drifting;
    public float Velocity = 2.0f;

    public ColliderEventReporter ColliderEventReport;
	public Collider2D ItemCollider;	

    public void Start()
    {
        ColliderEventReport.OnClickedSignal.AddListener(HandleOnClick);
    }

    private void HandleOnClick()
    {
        if (State == ItemState.Drifting)
        {
            GameplayController.PickItemSignal.Dispatch(this);            
		} else if (State == ItemState.Held) {
			if (BaseItem == null && AddOn.Base != null && AddOn.Base.Item.State == ItemState.Held) {
				AddOn.Base.Item.HandleOnClick ();
			} else if(BaseItem != null) {
				BaseItem.CombinationResult status = BaseItem.CheckCompletion ();
				if (status != BaseItem.CombinationResult.NotDone) {
					BaseItem.StopFlashing();
					GameplayController.TransmitterReadySignal.Dispatch ();
				}
			}
		}
    }

    public void Update()
    {
        if (State == ItemState.Held) return;
        
        Vector2 position = transform.position;
        position += Vector2.up * Time.deltaTime * Velocity;
        transform.position = position;
    }

    public ItemBehaviour Combine(ItemBehaviour other)
    {
        var combineBase = this;
        var newBit = other;
        if (combineBase.BaseItem == null)
        {
            combineBase = other;
            newBit = this;
        }
        
        combineBase.BaseItem.Attach(newBit);
        return combineBase;
    }

    public bool Combineable(ItemBehaviour other)
    {
        if (BaseItem == null && other.BaseItem == null)
        {
            return false;
        }

		if (BaseItem != null && other.BaseItem != null) 
		{
			return false;
		}

        if (AddOn != null && other.AddOn != null)
        {
            return false;
        }
        
        var combineBase = this;
        if (BaseItem == null)
        {
            combineBase = other;
        }

        return combineBase.BaseItem.first == null || combineBase.BaseItem.second == null; 
    }
}