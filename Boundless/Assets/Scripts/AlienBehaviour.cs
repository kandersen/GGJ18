using UnityEngine;

public class AlienBehaviour : MonoBehaviour
{
	public bool InFreeFall = true;
	private float _velocity = 2f;
	public float UpSpeed = 2f;
	public float DownSpeed = 4f;

	public Transform LeftHand;
	public Transform RightHand;

	public void Update ()
	{
		if (!InFreeFall) return;
		
		Vector2 position = transform.position;
		position += Vector2.down * Time.deltaTime * _velocity;
		transform.position = position;
	}
}
