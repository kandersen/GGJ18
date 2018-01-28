using UnityEngine;

public class AstronautAudio : MonoBehaviour
{
    public AudioClip PickupBatterySound;
    public AudioClip FlickSwitchSound;

    public AudioSource AudioSource;

    public void PlayPickupBattery()
    {
        AudioSource.PlayOneShot(PickupBatterySound);
    }

    public void PlayFlickSwitch()
    {
        AudioSource.PlayOneShot(FlickSwitchSound);
    }
}