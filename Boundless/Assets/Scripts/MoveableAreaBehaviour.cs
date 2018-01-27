using UnityEngine;

public class MoveableAreaBehaviour : MonoBehaviour
{
	public GameplayController GameplayController;
	
	public void OnMouseUpAsButton()
	{
		var screenPos = Input.mousePosition;
		var pos = Camera.main.ScreenToWorldPoint(screenPos);
		GameplayController.MoveSignal.Dispatch(pos);
	}
	
}
