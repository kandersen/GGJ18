using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MoveableAreaBehaviour : MonoBehaviour
{
	public GameplayController GameplayController;
	public Collider2D collider;

	public void Update()
	{
	}
	
	public void OnMouseUpAsButton()
	{
		var screenPos = Input.mousePosition;
		var pos = Camera.main.ScreenToWorldPoint(screenPos);
		GameplayController.MoveSignal.Dispatch(pos);
	}
	
}
