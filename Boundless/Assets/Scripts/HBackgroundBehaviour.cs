using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBackgroundBehaviour : MonoBehaviour {

	public GameObject background1;
	public GameObject background2;

	public float speed;

	private float height;
	// Use this for initialization
	void Start () {
		background1.transform.position = new Vector2 (-11.25f,0);
		Sprite spr1 = (background1.GetComponent<SpriteRenderer> ()).sprite;
		height = spr1.bounds.max.x;
		Debug.Log (height);
		background2.transform.position = new Vector2 (-11.25f-height,0);
	}

	// Update is called once per frame
	void Update () {
		Vector2 trans = new Vector2 (speed * Time.deltaTime,0);
		background1.transform.Translate (trans);
		background2.transform.Translate (trans);

		if (background1.transform.position.x > 11.25) {
			background1.transform.position =  new Vector2 (background2.transform.position.x - height,0);
		}
		if (background2.transform.position.x > 11.25) {
			background2.transform.position =  new Vector2 (background1.transform.position.x - height,0);
		}
	}
}
