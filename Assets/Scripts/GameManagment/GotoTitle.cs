using UnityEngine;

public class GotoTitle : MonoBehaviour
{
    public void CallGoToTitle()
    {
        GameManager.Instance.GotoMainMenu();
    }
}
