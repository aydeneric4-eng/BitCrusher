using UnityEngine;
using System;
using Unity.VisualScripting;

//[RequireComponent(typeof(EnemyM))]
[RequireComponent(typeof(HealthTracker))]
public class EnemyDeathHandler : MonoBehaviour, IHandlesDeath
{
    [SerializeField] int scoreReward = 100;
    [SerializeField] ParticleSystem deathEffect;
    private HealthTracker selfHPTracker;

    //public event Action enemyHasDied;
    [SerializeField] AudioClip deathSFX;

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
        GameManager.Instance.AddScore(scoreReward);
        //enemyHasDied?.Invoke();
        if (deathEffect)
            deathEffect.Play();
        if (deathSFX)
            GameManager.Instance.audioManager.PlaySFX(deathSFX);
        Destroy(gameObject);
    }

}
