using NUnit.Framework.Constraints;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Signal MoveDownwardsSignal = new Signal();

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
        Debug.Log("HandleMoveDown");
    }
}