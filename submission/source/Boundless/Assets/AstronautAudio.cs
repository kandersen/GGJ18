using UnityEngine;

public class AstronautAudio : MonoBehaviour
{
    public AudioClip PickupBatterySound;
    public AudioClip FlickSwitchSound;
    public AudioClip CombineSuccessSound;
    public AudioClip CombineFailureSound;

    public AudioSource AudioSource;
    
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
}