using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameplayController GameplayController;
    
    public void OnMouseUpAsButton()
    {
        GameplayController.PickItemSignal(this);
    }    
}
