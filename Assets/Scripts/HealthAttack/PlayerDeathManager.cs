using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(HealthTracker))]
public class PlayerDeathManager : MonoBehaviour
{
    private HealthTracker selfHPTracker;
    private void Awake()
    {
        selfHPTracker = GetComponent<HealthTracker>();
        selfHPTracker.healthAtZero += Die;
    }

    public void Die()
    {
        GameManager.ReducePlayerLives(-1);
        if (GameManager.playerLives < 1)
        {
            Destroy(gameObject);
        }
        selfHPTracker.MakeInvincible(2f);
        selfHPTracker.ResetHealth();
    }
}