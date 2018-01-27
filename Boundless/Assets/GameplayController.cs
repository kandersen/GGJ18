using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Signal MoveDownwardsSignal;

    public void ConnectSignals()
    {
        MoveDownwardsSignal.AddListener(HandleMoveDown);
    }

    private void HandleMoveDown()
    {
        throw new System.NotImplementedException();
    }
}