using UnityEngine;

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

    public AddOn first;
    public AddOn second;

    public AddOn Attach(AddOn addOn)
    {
        if (first == null)
        {
            addOn.transform.parent = JoinPoint1;
            addOn.transform.position = Vector3.zero;
            first = addOn;
            return null;
        }
        
        if (second == null)
        {
            addOn.transform.parent = JoinPoint1;
            addOn.transform.position = Vector3.zero;
            second = addOn;
            return null;
        }
                
        return addOn;        
    }

    public CombinationResult CheckCompletion()
    {
        if (first == null || second == null)
        {
            return CombinationResult.NotDone;
        }
        
        var switchable = first.Class == AddOn.ItemClass.Switch ||
                         second.Class == AddOn.ItemClass.Switch;

        if (!switchable) 
            return CombinationResult.Dud;
        
        if (first.Class == AddOn.ItemClass.Antenna || second.Class == AddOn.ItemClass.Antenna)
        {
            return CombinationResult.Success;
        }
        return CombinationResult.Switchable;
    }
}