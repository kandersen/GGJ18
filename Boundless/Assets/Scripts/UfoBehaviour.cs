using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UfoBehaviour : MonoBehaviour {

	public SpriteRenderer boomRenderer;
	public SpriteRenderer ufoNormalRenderer;
	public SpriteRenderer ufoWhiteRenderer;
	public SpriteRenderer astronautRenderer;
	public SpriteRenderer boundlessRenderer;

	public MeshRenderer blackRenderer;

	public Rigidbody2D astronautRB2D;

	public HBackgroundBehaviour background;	

	public ColliderEventReporter cer;

	public AudioSource AudioSource;
	public AudioClip ExplosionClip;

	private Coroutine scene = null;

	// Use this for initialization
	void Start () {
		astronautRB2D.simulated = false;
		cer.OnTriggerSignal.AddListener (HandleTrigger);
		ufoWhiteRenderer.color = new Color (1, 1, 1, 0);
		blackRenderer.material.color = new Color (0, 0, 0, 0);
	}

	void OnMouseUpAsButton () {
		Debug.Log("Yes, on mouse up as button works.");
		if (scene == null) {
			scene = StartCoroutine (AndAction ());
		}

	}

	IEnumerator AndAction() {
		yield return DOTween.ToAlpha (() => boundlessRenderer.color, x => boundlessRenderer.color = x,0,2f);

		yield return new WaitForSeconds (1);

		DOTween.To(() => AudioSource.volume, v => AudioSource.volume = v, 0f, 2.8f);
		
		yield return DOTween.ToAlpha (() => ufoWhiteRenderer.color, x => ufoWhiteRenderer.color = x, 1, 2.8f).SetEase(Ease.InQuart).WaitForCompletion();

		AudioSource.Stop();

		yield return new WaitForSeconds (0.2f);

		ufoNormalRenderer.enabled = false;
		ufoWhiteRenderer.enabled = false;
		boomRenderer.gameObject.transform.position = ufoWhiteRenderer.gameObject.transform.position;
		boomRenderer.enabled = true;

		AudioSource.volume = 1.0f;
		AudioSource.PlayOneShot(ExplosionClip);

		yield return DOTween.To (() => background.speed, x => background.speed = x, 0, 0.5f).WaitForCompletion();


		astronautRenderer.enabled = true;
		astronautRenderer.gameObject.transform.position = new Vector2 (0.2f, 0.2f);
		astronautRB2D.simulated = true;
		astronautRB2D.velocity = new Vector2 (4, 5);
		astronautRB2D.angularVelocity = -100f;

		DOTween.ToAlpha (() => boomRenderer.color, x => boomRenderer.color = x,0,3.5f).SetEase(Ease.OutCubic);
	}

	IEnumerator End() {
		yield return DOTween.ToAlpha (() => blackRenderer.material.color, x => blackRenderer.material.color = x,1,0.5f).WaitForCompletion();

		SceneManager.LoadSceneAsync ("Main");

	}

	void HandleTrigger() {
		StartCoroutine( End ());
	}
}
