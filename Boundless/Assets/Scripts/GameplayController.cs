using System.Collections;
using DG.Tweening;
using NUnit.Framework.Constraints;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Signal MoveDownwardsSignal = new Signal();

    private Coroutine _activeRoutine;

    public AlienBehaviour Alien;
    public BackgroundBehaviour Background;
    public Transform AlienStartPosition;
    
    public void Start()
    {
        ConnectSignals();
    }
    
    public void ConnectSignals()
    {
        MoveDownwardsSignal.AddListener(HandleMoveDown);
    }
    
    

    private void HandleMoveDown()
    {
        if (_activeRoutine != null)
        {
            StopCoroutine(_activeRoutine);
        }
        DisconnectSignals();
        _activeRoutine = StartCoroutine(TransitionRoutine());
        
    }

    private void DisconnectSignals()
    {
        MoveDownwardsSignal.RemoveListener(HandleMoveDown);
    }

    private IEnumerator TransitionRoutine()
    {
        Alien.InFreeFall = false;
        DOTween.To(()=> Background.speed, x=> Background.speed = x, 2.0f, 1).SetEase(Ease.InOutCubic);
        yield return Alien.transform.DOMove(AlienStartPosition.position, 3.0f);
        DOTween.To(()=> Background.speed, x=> Background.speed = x, 0.2f, 1).SetEase(Ease.InOutCubic);
        Alien.InFreeFall = true;
        ConnectSignals();
    }
}