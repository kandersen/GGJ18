using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSetup : MonoBehaviour {

	public MeshRenderer renderer;

	// Use this for initialization
	void Start () {
		Debug.Log (renderer.sortingLayerName);
		renderer.sortingLayerName = "Fade";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
