using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Signal MoveDownwardsSignal = new Signal();

    private Coroutine _activeCommand;

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
        if (_activeCommand == null)
        {
            _activeCommand = StartCoroutine(TransitionRoutine());            
        }
    }
    
    private IEnumerator TransitionRoutine()
    {
        Alien.InFreeFall = false;
        DOTween.To(()=> Background.speed, x=> Background.speed = x, 2.0f, 0.5f);
        yield return Alien.transform.DOMove(AlienStartPosition.position, 3.0f).WaitForCompletion();
        DOTween.To(()=> Background.speed, x=> Background.speed = x, 0.2f, 0.5f);
        Alien.InFreeFall = true;
        _activeCommand = null;
    }


}
