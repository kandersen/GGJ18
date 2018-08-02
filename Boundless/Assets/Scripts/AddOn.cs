using UnityEngine;

public class AddOn : MonoBehaviour
{
    public ItemBehaviour Item;
	public BaseItem Base;
	public SpriteRenderer FlashRenderer;
	public Animator TransmitionAnimation;

	public enum ItemClass
    {
        Switch = 0,
        Antenna = 1,
        Garbage = 2,
    }

	public void Start() {
		if (FlashRenderer != null) {
			FlashRenderer.enabled = false;
		}
	}

	public void Transmit()
	{
		if (Class != ItemClass.Antenna) {
			Debug.Log ("Not an antenna");
			return;
		}

		TransmitionAnimation.SetBool ("transmit", true);
	}
		
	public void TransmitStop()
	{
		if (Class != ItemClass.Antenna) {
			Debug.Log ("Not an antenna");
			return;
		}

		TransmitionAnimation.SetBool ("transmit", false);
	}

    public ItemClass Class;
}