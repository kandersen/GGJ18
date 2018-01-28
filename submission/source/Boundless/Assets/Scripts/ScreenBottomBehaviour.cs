using UnityEngine;

public class ScreenBottomBehaviour : MonoBehaviour
{
    public GameplayController GameplayController;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.NAUT))
        {
            GameplayController.MoveDownwardsSignal.Dispatch();
        }
    }

    public void OnMouseUpAsButton()
    {
        GameplayController.MoveDownwardsSignal.Dispatch();
    }

}