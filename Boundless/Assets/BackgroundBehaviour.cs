using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour {

	public GameObject background1;
	public GameObject background2;

	public float speed;

	private float height;
	// Use this for initialization
	void Start () {
		background1.transform.position = new Vector2 (0,7);
		Sprite spr1 = (background1.GetComponent<SpriteRenderer> ()).sprite;
		height = spr1.bounds.max.y;
		Debug.Log (height);
		background2.transform.position = new Vector2 (0, 7 - height);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 trans = new Vector2 (0, speed);
		background1.transform.Translate (trans);
		background2.transform.Translate (trans);
		if (background1.transform.position.y > 7.0) {
			background1.transform.position =  new Vector2 (0,background2.transform.position.y - height);
			Debug.Log (1);
		}
		if (background2.transform.position.y > 7.0) {
			background2.transform.position =  new Vector2 (0,background1.transform.position.y - height);
			Debug.Log (2);
		}
	}
}
