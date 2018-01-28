using UnityEngine;

public class ColliderEventReporter : MonoBehaviour {

	public Signal OnClickedSignal = new Signal();
	public Signal OnTriggerSignal = new Signal();

	public void OnMouseUpAsButton()
	{
		OnClickedSignal.Dispatch();
	}

	public void OnTriggerEnter2D(Collider2D c)
	{
		OnTriggerSignal.Dispatch ();
	}
}
