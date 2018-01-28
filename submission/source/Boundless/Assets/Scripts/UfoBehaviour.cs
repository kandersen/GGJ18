using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UfoBehaviour : MonoBehaviour {

	public SpriteRenderer BoomRenderer;
	public SpriteRenderer UfoNormalRenderer;
	public SpriteRenderer UfoWhiteRenderer;
	public SpriteRenderer AstronautRenderer;
	public SpriteRenderer BoundlessRenderer;
	public SpriteRenderer BeamRenderer;
	public SpriteRenderer BeamLineRenderer;

	public MeshRenderer BlackRenderer;

	public Rigidbody2D AstronautRB2D;

	public HBackgroundBehaviour Background;	

	public ColliderEventReporter Cer;

	public AudioSource AudioSource;
	public AudioClip ExplosionClip;

	private Coroutine Scene = null;
	private Coroutine Beam = null;
	 
	// Use this for initialization
	void Start () {
		Debug.Log ("Game started: " + PersistentData.GameStarted);
		if (PersistentData.GameStarted) {
			BoundlessRenderer.enabled = false;
		} else {
			BeamLineRenderer.color = new Color (1, 1, 1, 0);
			BeamRenderer.color = new Color (1, 1, 1, 0);
		}
		AstronautRB2D.simulated = false;
		Cer.OnTriggerSignal.AddListener (HandleTrigger);
		UfoWhiteRenderer.color = new Color (1, 1, 1, 0);
		BlackRenderer.material.color = new Color (0, 0, 0, 0);

	} 

	void OnMouseUpAsButton () {
		Debug.Log("Yes, on mouse up as button works.");
		if (Scene == null) {
			Scene = StartCoroutine (AndAction ());
		}

	}

	void Update() {
		if (PersistentData.GameStarted && Beam == null && Scene == null) {
			Beam = StartCoroutine (PlayBeam ());
		}
	}


	IEnumerator PlayBeam() {
		

		BeamLineRenderer.color = new Color (1, 1, 1, 1);
		BeamRenderer.color = new Color (1, 1, 1, 0.5f);

		yield return new WaitForSeconds (1f);
		AstronautRenderer.enabled = true;
		Vector2 astroPos = AstronautRenderer.gameObject.transform.position;
		Quaternion astroRot = AstronautRenderer.gameObject.transform.rotation;
		Vector3 astroScale = AstronautRenderer.gameObject.transform.localScale;
		int astroSort = AstronautRenderer.sortingOrder;

		AstronautRenderer.gameObject.transform.position = new Vector2(UfoNormalRenderer.transform.position.x,-7f);
		AstronautRenderer.gameObject.transform.rotation = Quaternion.identity;
		AstronautRenderer.gameObject.transform.localScale = new Vector3 (0.2f, 0.2f, 0f);
		AstronautRenderer.sortingOrder = 7;


		yield return AstronautRenderer.gameObject.transform.DOMove (new Vector3(0,0,0),8f).WaitForCompletion();


		AstronautRenderer.enabled = false;
		AstronautRenderer.gameObject.transform.position = astroPos;
		AstronautRenderer.gameObject.transform.rotation = astroRot;
		AstronautRenderer.gameObject.transform.localScale = astroScale;
		AstronautRenderer.sortingOrder = astroSort;

		DOTween.ToAlpha (() => BeamLineRenderer.color, x => BeamLineRenderer.color = x, 0, 0.2f);
		DOTween.ToAlpha (() => BeamRenderer.color, x => BeamRenderer.color = x, 0, 0.2f);
		BeamLineRenderer.enabled = false;
		BeamRenderer.enabled = false;

		yield return null;
	}

	IEnumerator AndAction() {
		if (!PersistentData.GameStarted) {
			yield return DOTween.ToAlpha (() => BoundlessRenderer.color, x => BoundlessRenderer.color = x, 0, 2f);
		}
		yield return new WaitForSeconds (1);

		DOTween.To(() => AudioSource.volume, v => AudioSource.volume = v, 0f, 2.8f);
		
		yield return DOTween.ToAlpha (() => UfoWhiteRenderer.color, x => UfoWhiteRenderer.color = x, 1, 2.8f).SetEase(Ease.InQuart).WaitForCompletion();

		AudioSource.Stop();

		yield return new WaitForSeconds (0.2f);

		UfoNormalRenderer.enabled = false;
		UfoWhiteRenderer.enabled = false;
		BoomRenderer.gameObject.transform.position = UfoWhiteRenderer.gameObject.transform.position;
		BoomRenderer.enabled = true;

		AudioSource.volume = 1.0f;
		AudioSource.PlayOneShot(ExplosionClip);

		yield return DOTween.To (() => Background.speed, x => Background.speed = x, 0, 0.5f).WaitForCompletion();


		AstronautRenderer.enabled = true;
		AstronautRenderer.gameObject.transform.position = new Vector2 (0.2f, 0.2f);
		AstronautRB2D.simulated = true;
		AstronautRB2D.velocity = new Vector2 (4, 5);
		AstronautRB2D.angularVelocity = -100f;

		DOTween.ToAlpha (() => BoomRenderer.color, x => BoomRenderer.color = x,0,3.5f).SetEase(Ease.OutCubic);
	}

	IEnumerator End() {
		yield return DOTween.ToAlpha (() => BlackRenderer.material.color, x => BlackRenderer.material.color = x,1,0.5f).WaitForCompletion();

		PersistentData.GameStarted = true;


		//SceneManager.LoadSceneAsync ("Intro");
		SceneManager.LoadSceneAsync ("Main");


	}

	void HandleTrigger() {
		StartCoroutine( End ());
	}
}
