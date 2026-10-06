using UnityEngine;

public class NextLv : MonoBehaviour
{
    public void CallNextLevel()
    {
        GameManager.Instance.GotoNextLevel();
    }
}
