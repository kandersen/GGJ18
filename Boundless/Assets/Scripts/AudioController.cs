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

	public AudioSource BackgroundMusic;
	public AudioSource SereneMusic;

	public JetpackSound JetPackSound;

	public AudioSource AudioSource;

	public void PlayTheme() {
		SereneMusic.Play ();
	}

	public IEnumerator FadeBackgroundMusic(float fadeTime) {
		yield return BackgroundMusic.DOFade (0, fadeTime).WaitForCompletion();
		yield return null;
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
}
