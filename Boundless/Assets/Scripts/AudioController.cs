using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using DG.Tweening;

public class AudioController : MonoBehaviour {

	// SFX
	[EventRef]
	public string PickupBatterySound;
	[EventRef]
	public string FlickSwitchSound;
	[EventRef]
	public string CombineSuccessSound;
	[EventRef]
	public string CombineFailureSound;
	[EventRef]
	public string TransmitSound;
	[EventRef]
	public string TransmitterReadySound;
	[EventRef]
	public string JetPackSound;

	// Soundtrack
	[EventRef]
	public string BackgroundMusic;
	[EventRef]
	public string SereneMusic;

	private EventInstance _backgroundMusicEmitter;
	private EventInstance _sereneMusicEmitter;
	private EventInstance _jetpackSoundEmitter;

	void Start(){
		_backgroundMusicEmitter = RuntimeManager.CreateInstance(BackgroundMusic);
		_sereneMusicEmitter = RuntimeManager.CreateInstance(SereneMusic);

		_backgroundMusicEmitter.start();
	}

	public void PlayTheme() {
		_sereneMusicEmitter.start();
	}

	public void FadeBackgroundMusic() {
		_backgroundMusicEmitter.stop(STOP_MODE.ALLOWFADEOUT);
		_backgroundMusicEmitter.release();
	}

	public void PlayJetPackSound() {
		_jetpackSoundEmitter = RuntimeManager.CreateInstance(JetPackSound);
		_jetpackSoundEmitter.start();
	}

	public void StopJetPackSound() {
		_jetpackSoundEmitter.stop(STOP_MODE.ALLOWFADEOUT);
		_jetpackSoundEmitter.release();
	}

	public void PlayPickupBattery()
	{
		RuntimeManager.PlayOneShot(PickupBatterySound);
	}

	public void PlayFlickSwitch()
	{
		RuntimeManager.PlayOneShot(FlickSwitchSound);
	}

	public void PlayCombineSuccess()
	{
		RuntimeManager.PlayOneShot(CombineSuccessSound);
	}

	public void PlayCombineFailure()
	{
		RuntimeManager.PlayOneShot(CombineFailureSound);
	}

	public void PlayTransmit() {
		RuntimeManager.PlayOneShot (TransmitSound);
	}

	public void PlayTransmitterReady(){
		RuntimeManager.PlayOneShot (TransmitterReadySound);
	}
}
