using UnityEngine;

//[RequireComponent(typeof(EnemyM))]
[RequireComponent(typeof(HealthTracker))]
public class EnemyDeathHandler : MonoBehaviour, IHandlesDeath
{
    [SerializeField] int scoreReward = 100;
    private HealthTracker selfHPTracker;

    private void Awake()
    {
        selfHPTracker = GetComponent<HealthTracker>();
        selfHPTracker.healthAtZero += Die;
    }

    public void Die()
    {
        GameManager.Instance.AddScore(scoreReward);
        Destroy(gameObject);
    }

}
