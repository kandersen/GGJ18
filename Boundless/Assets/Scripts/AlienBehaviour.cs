using UnityEngine;

public class AlienBehaviour : MonoBehaviour
{
	public bool InFreeFall = true;
	private float velocity = 2f;

	public void Update ()
	{
		if (InFreeFall)
		{
			Vector2 position = transform.position;
			position += Vector2.down * Time.deltaTime * velocity;
			transform.position = position;
		}
	}
}
