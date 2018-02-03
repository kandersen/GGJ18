using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioController : MonoBehaviour {

	public AudioClip PickupBatterySound;
	public AudioClip FlickSwitchSound;
	public AudioClip CombineSuccessSound;
	public AudioClip CombineFailureSound;
	public AudioClip TransmitSound;
	public AudioClip TransmitterReadySound;

	public AudioSource BackgroundMusic;
	public AudioSource BackgroundMusicv2;
	public AudioSource SereneMusic;

	public JetpackSound JetPackSound;

	public AudioSource AudioSource;

	public void PlayTheme() {
		SereneMusic.Play ();
	}

	public IEnumerator FadeBackgroundMusic(float fadeTime) {
		yield return BackgroundMusicv2.DOFade (0, fadeTime).WaitForCompletion();
	}

	public void PlayJetPackSound() {
		JetPackSound.Play ();
	}

	public void StopJetPackSound() {
		JetPackSound.Stop ();
	}

	public void PlayPickupBattery()
	{
		AudioSource.PlayOneShot(PickupBatterySound);
	}

	public void PlayFlickSwitch()
	{
		AudioSource.PlayOneShot(FlickSwitchSound);
	}

	public void PlayCombineSuccess()
	{
		AudioSource.PlayOneShot(CombineSuccessSound);
	}

	public void PlayCombineFailure()
	{
		AudioSource.PlayOneShot(CombineFailureSound);
	}

	public void PlayTransmit() {
		AudioSource.PlayOneShot (TransmitSound);
	}

	public void PlayTransmitterReady(){
		AudioSource.PlayOneShot (TransmitterReadySound);
	}
}
