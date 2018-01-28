using UnityEngine;

public class ScreenTopBehaviour : MonoBehaviour
{
    public GameplayController GameplayController;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.ITEM))
        {
            var item = other.gameObject.GetComponentInParent<ItemBehaviour>();
            GameplayController.ItemDriftedOffScreenSignal.Dispatch(item);
        }
    }
}