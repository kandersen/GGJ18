using UnityEngine;

public class ColliderEventReporter : MonoBehaviour {

	public Signal OnClickedSignal = new Signal();

	public void OnMouseUpAsButton()
	{
		OnClickedSignal.Dispatch();
	}
}
