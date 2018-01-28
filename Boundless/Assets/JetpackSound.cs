using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JetpackSound : MonoBehaviour
{
    public AudioSource AudioSource;
    public Coroutine _routine;
    
    public void Play()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }
        AudioSource.volume = 1.0f;
        AudioSource.Play();
    }

    public void Stop()
    {
        if (_routine == null)
        {
            _routine = StartCoroutine(FadeSoundRoutine());            
        }
    }

    private IEnumerator FadeSoundRoutine()
    {
        yield return DOTween.To(() => AudioSource.volume, v => AudioSource.volume = v, 0f, 0.2f).WaitForCompletion();
        _routine = null;
    }
}