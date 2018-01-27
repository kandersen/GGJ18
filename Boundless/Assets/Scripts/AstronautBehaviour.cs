using System.Collections.Generic;
using UnityEngine;

public class AstronautBehaviour : MonoBehaviour
{
	public bool InFreeFall = true;
	private float _velocity = 2f;
	public float UpSpeed = 2f;
	public float DownSpeed = 4f;

	public Stack<ItemBehaviour> Items = new Stack<ItemBehaviour>();
	
	public Transform LeftHand;
	public Transform RightHand;

	public ItemBehaviour PickUpItem(ItemBehaviour item)
	{
		ItemBehaviour result = null;
		switch (Items.Count)
		{
				case 0:
					Items.Push(item);
					item.transform.parent = LeftHand.transform;
					item.transform.localPosition = Vector3.zero;
					item.transform.rotation = Quaternion.identity;
					item.DriftBehaviour.enabled = false;
					item.State = ItemBehaviour.ItemState.Held;					
					break;
			    case 1:
				    Items.Push(item);
				    item.transform.parent = RightHand.transform;
				    item.transform.localPosition = Vector3.zero;
				    item.transform.rotation = Quaternion.identity;
				    item.DriftBehaviour.enabled = false;
				    item.State = ItemBehaviour.ItemState.Held;
				    break;
				default:
					result = Items.Pop();
					result.transform.parent = null;					
					result.State = ItemBehaviour.ItemState.Dropped;
					Items.Push(item);
					item.transform.parent = RightHand.transform;
					item.transform.localPosition = Vector3.zero;
					item.transform.rotation = Quaternion.identity;
					item.DriftBehaviour.enabled = false;
					item.State = ItemBehaviour.ItemState.Held;
					break;
		}
		return result;
	}
	
	public void Update ()
	{
		if (!InFreeFall) return;		
		Vector2 position = transform.position;
		position += Vector2.down * Time.deltaTime * _velocity;
		transform.position = position;
	}
}
