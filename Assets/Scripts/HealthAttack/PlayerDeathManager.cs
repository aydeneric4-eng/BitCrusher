using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(HealthTracker))]
public class PlayerDeathManager : MonoBehaviour, IHandlesDeath
{
    [SerializeField] ParticleSystem deathVFX;
    private HealthTracker selfHPTracker;
    private Vector3 ogPosition;
    private void Awake()
    {
        selfHPTracker = GetComponent<HealthTracker>();
        selfHPTracker.healthAtZero += DeathCoupler;

        ogPosition = transform.position;
    }
    public void DeathCoupler() => Die();
    public void Die(bool superDie = false)
    {
        if (superDie)
        {
            transform.position = ogPosition;
            return;
        }

        if (deathVFX)
        {
            deathVFX.Play();
        }

        GameManager.Instance.ReducePlayerLives(-1);

        if (GameManager.Instance.playerLives < 1)
        {
            GameManager.Instance.GotoLossScreen();
            Destroy(gameObject);
        }

        selfHPTracker.MakeInvincible(2f);
        selfHPTracker.ResetHealth();
    }
}