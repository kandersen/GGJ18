using UnityEngine;

public class ScreenTopBehaviourAstronaut : MonoBehaviour
{
    public GameplayController GameplayController;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log ("astro top box");
        if (other.CompareTag(Tags.NAUT))
        {
			Debug.Log ("astro top box triggered");
			GameplayController.AstronautTopOfScreen.Dispatch ();
        }
    }
}