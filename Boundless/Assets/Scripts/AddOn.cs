using UnityEngine;

public class AddOn : MonoBehaviour
{
    public ItemBehaviour Item;
	public BaseItem Base;
	public SpriteRenderer FlashRenderer;

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

    public ItemClass Class;
}