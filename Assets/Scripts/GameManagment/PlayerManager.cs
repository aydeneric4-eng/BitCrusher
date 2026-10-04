using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.playerIntance = this;
    }
}
