using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void CallStartGame()
    {
        GameManager.Instance.StartGame();
    }
}
