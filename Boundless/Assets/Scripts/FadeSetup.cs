using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSetup : MonoBehaviour {

	public MeshRenderer MeshRenderer;

	// Use this for initialization
	void Start () {
		Debug.Log (MeshRenderer.sortingLayerName);
		MeshRenderer.sortingLayerName = "Fade";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
