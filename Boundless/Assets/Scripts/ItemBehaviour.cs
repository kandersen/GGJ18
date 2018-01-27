using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public enum ItemState
    {
        Drifting = 0,
        Held = 1,
        Dropped = 2,
    }
    
    public GameplayController GameplayController;
    public DriftBehaviour DriftBehaviour;
    public ItemState State = ItemState.Drifting;
    public float Velocity = 2.0f;

    public void OnMouseUpAsButton()
    {
        Debug.Log("pressed!");
        if (State == ItemState.Drifting)
        {        
            GameplayController.PickItemSignal.Dispatch(this);            
        }
    }

    public void Update()
    {
        if (State != ItemState.Drifting) return;
        
        Vector2 position = transform.position;
        position += Vector2.up * Time.deltaTime * Velocity;
        transform.position = position;
    }
    
}
