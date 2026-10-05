using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }
}
