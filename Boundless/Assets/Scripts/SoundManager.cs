using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, EventInstance> _instances;
    
    public void Start()
    {
        _instances = new Dictionary<string, EventInstance>();
    }

    public void StartEvent(string sound)
    {
        var instance = RuntimeManager.CreateInstance(sound);
        _instances[sound] = instance;
        instance.start();
    }

    public void FadeEvent(string sound)
    {
        _instances[sound].stop(STOP_MODE.ALLOWFADEOUT);
        _instances.Remove(sound);
    }
    
    public void StopEvent(string sound)
    {        
        _instances[sound].stop(STOP_MODE.IMMEDIATE);
        _instances.Remove(sound);
    }
    
    public void PlayOneShot(string explosionSoundEvent)
    {
        RuntimeManager.PlayOneShot(explosionSoundEvent);
    }
}