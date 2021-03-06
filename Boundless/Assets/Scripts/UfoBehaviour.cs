﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Debug = UnityEngine.Debug;

public class UfoBehaviour : MonoBehaviour
{
    public PersistentDataManager PersistentDataManager;
    public SoundManager SoundManager;
    
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

    private Coroutine _scene = null;
    private Coroutine _beam = null;
	 
    private bool _canStart = false;

    [EventRef]
    public string PlayIntroMusicEvent;
    [EventRef]
    public string ExplosionSoundEvent;
    
    // Use this for initialization
    public void Start ()
    {
        PersistentDataManager = FindObjectOfType<PersistentDataManager>();
        SoundManager = FindObjectOfType<SoundManager>();

        SoundManager.StartEvent(PlayIntroMusicEvent);
		
        Debug.Log ("Game started: " + PersistentDataManager.GameStarted);
        if (PersistentDataManager.GameStarted) {
            BoundlessRenderer.enabled = false;
        } else {
            BeamLineRenderer.color = new Color (1, 1, 1, 0);
            BeamRenderer.color = new Color (1, 1, 1, 0);
            _canStart = true;
        }
        AstronautRB2D.simulated = false;
        Cer.OnTriggerSignal.AddListener (HandleTrigger);
        UfoWhiteRenderer.color = new Color (1, 1, 1, 0);
        BlackRenderer.material.color = new Color (0, 0, 0, 0);

    } 

    void OnMouseUpAsButton () {
        Debug.Log("Yes, on mouse up as button works.");
        if (_scene == null  && _canStart) {
            _scene = StartCoroutine (AndAction ());
        }

    }

    void Update() {
        if (PersistentDataManager.GameStarted && _beam == null && _scene == null) {
            _beam = StartCoroutine (PlayBeam ());
        }
    }


    public IEnumerator PlayBeam() {		
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
        _canStart = true;
        yield return null;
    }

    private IEnumerator AndAction() {
        if (!PersistentDataManager.GameStarted) {
            yield return DOTween.ToAlpha (() => BoundlessRenderer.color, x => BoundlessRenderer.color = x, 0, 2f);
        }
        yield return new WaitForSeconds (1);

        SoundManager.FadeEvent(PlayIntroMusicEvent);
         
        yield return DOTween.ToAlpha (() => UfoWhiteRenderer.color, x => UfoWhiteRenderer.color = x, 1, 2.8f).SetEase(Ease.InQuart).WaitForCompletion();
        
        yield return new WaitForSeconds (0.2f);

        UfoNormalRenderer.enabled = false;
        UfoWhiteRenderer.enabled = false;
        BoomRenderer.gameObject.transform.position = UfoWhiteRenderer.gameObject.transform.position;
        BoomRenderer.enabled = true;

        SoundManager.PlayOneShot(ExplosionSoundEvent);

        yield return DOTween.To (() => Background.speed, x => Background.speed = x, 0, 0.5f).WaitForCompletion();


        AstronautRenderer.enabled = true;
        AstronautRenderer.gameObject.transform.position = new Vector2 (0.2f, 0.2f);
        AstronautRB2D.simulated = true;
        AstronautRB2D.velocity = new Vector2 (4, 5);
        AstronautRB2D.angularVelocity = -100f;

        DOTween.ToAlpha (() => BoomRenderer.color, x => BoomRenderer.color = x,0,3.5f).SetEase(Ease.OutCubic);
    }

    private IEnumerator End() {
        yield return DOTween.ToAlpha (() => BlackRenderer.material.color, x => BlackRenderer.material.color = x,1,0.5f).WaitForCompletion();
        PersistentDataManager.GameStarted = true;
        SceneManager.LoadSceneAsync ("Main");
    }

    void HandleTrigger() {
        StartCoroutine( End ());
    }
}