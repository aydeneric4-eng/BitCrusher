using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        GameManager.playerIntance = this;
    }
}
