using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(HealthTracker))]
public class PlayerDeathManager : MonoBehaviour, IHandlesDeath
{
    private HealthTracker selfHPTracker;
    private void Awake()
    {
        selfHPTracker = GetComponent<HealthTracker>();
        selfHPTracker.healthAtZero += DeathCoupler;
    }
    public void DeathCoupler() => Die();
    public void Die(bool superDie = false)
    {
        if (superDie)
        {
            Debug.Log("superDie");
            GameManager.Instance.ReducePlayerLives(-999);
        }
        GameManager.Instance.ReducePlayerLives(-1);
        Debug.Log("Reduced life");
        if (GameManager.Instance.playerLives < 1)
        {
            GameManager.Instance.GotoLossScreen();
            Destroy(gameObject);
        }

        selfHPTracker.MakeInvincible(2f);
        selfHPTracker.ResetHealth();
    }
}