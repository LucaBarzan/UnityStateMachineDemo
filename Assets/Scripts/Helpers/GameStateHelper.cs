using UnityEngine;

public class GameStateHelper : MonoBehaviour
{
    public GameState GameState;

    public void SetGameState()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.State = GameState;
    }
}