﻿using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public ItemBehaviour Item;
    
    public Transform JoinPoint1;
    public Transform JoinPoint2;

    public enum CombinationResult
    {
        NotDone = 0,
        Success = 1,
        Switchable = 2,
        Dud = 3,
    }

    public ItemBehaviour first;
    public ItemBehaviour second;

    public void Attach(ItemBehaviour item)
    {
		if (second == null && item.AddOn.Class == AddOn.ItemClass.Antenna) {
			item.transform.parent = JoinPoint2;
			item.transform.localPosition = Vector3.zero;
			item.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 30));
			second = item;
			item.AddOn.Base = this;
		}
		else if (first == null && item.AddOn.Class == AddOn.ItemClass.Switch) {
			item.transform.parent = JoinPoint1;
			item.transform.localPosition = Vector3.zero;
			item.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 10));
			first = item;
			item.AddOn.Base = this;
		} 
		else if (first == null)
        {
            item.transform.parent = JoinPoint1;
            item.transform.localPosition = Vector3.zero;
            first = item;
			item.AddOn.Base = this;
        }
        else
        {
            item.transform.parent = JoinPoint2;
            item.transform.localPosition = Vector3.zero;
            second = item;
			item.AddOn.Base = this;
        }
      
    }

    public CombinationResult CheckCompletion()
    {
        if (first == null || second == null )
        {
            return CombinationResult.NotDone;
        }               

		if (first.AddOn == null || second.AddOn == null) {
			return CombinationResult.NotDone;
		}
        
        var switchable = first.AddOn.Class == AddOn.ItemClass.Switch ||
                         second.AddOn.Class == AddOn.ItemClass.Switch;

        if (!switchable) 
            return CombinationResult.Dud;

        var hasAntenna = first.AddOn.Class == AddOn.ItemClass.Antenna ||
                         second.AddOn.Class == AddOn.ItemClass.Antenna; 
        
        return hasAntenna ? CombinationResult.Success : CombinationResult.Switchable;
    }
}