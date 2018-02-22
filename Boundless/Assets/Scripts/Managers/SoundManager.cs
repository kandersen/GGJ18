using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
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

    public EventWrapper FadeEvent(string sound)
    {
        if (_instances.ContainsKey(sound))
        {
            var instance = _instances[sound];
            instance.stop(STOP_MODE.ALLOWFADEOUT);
            return new EventWrapper(instance);
        }
        else return null;
    }
    
    public void StopEvent(string sound)
    {
        var instance = _instances[sound];
        if (_instances.Remove(sound))
        {
            instance.stop(STOP_MODE.IMMEDIATE);
            instance.release();            
        }
    }
    
    public void PlayOneShot(string explosionSoundEvent)
    {
        RuntimeManager.PlayOneShot(explosionSoundEvent);
    }
}

public class EventWrapper
{
    private EventInstance EventInstance;
    private bool _done;
    
    public EventWrapper(EventInstance instance)
    {
        _done = false;
        EventInstance = instance;
        EventInstance.setCallback(Callback);
    }

    private RESULT Callback(EVENT_CALLBACK_TYPE type, EventInstance eventinstance, IntPtr parameters)
    {
        if (type == EVENT_CALLBACK_TYPE.STOPPED) 
            _done = true;
        
        return RESULT.OK;
    }

    public IEnumerator AwaitCompletion()
    {
        while (!_done)
        {
            yield return null;
        }
            
    }
}