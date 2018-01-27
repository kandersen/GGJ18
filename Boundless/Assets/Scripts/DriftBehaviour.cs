using UnityEngine;

public class DriftBehaviour : MonoBehaviour
{
	public float RotationalVelocity;
	
	// Use this for initialization
	void Start ()
	{
		RotationalVelocity = (Random.value - 0.5f) * 30f;
		transform.Rotate(Vector3.back * Random.value);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.back, RotationalVelocity * Time.deltaTime);
	}
}
