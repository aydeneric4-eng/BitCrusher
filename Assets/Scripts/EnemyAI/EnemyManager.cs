using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerManager player;

    [SerializeField] OrbitAnchorAndAimAtTarget face;
    [SerializeField] OrbitAnchorAndAimAtTarget barrel;

    bool isSetup = false;

    void Start()
    {
        
    }

    private void Update()
    {
        if (!player)
        {
            player = GameManager.Instance.playerIntance;
        } else
        {
            return;
        }
        if (isSetup || !player)
            return;
        if (face)
            face.targetTransform = player.transform;
        if (barrel)
            barrel.targetTransform = player.transform;
        isSetup = true;
    }
}
