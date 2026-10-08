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

    [SerializeField] GameObject[] outsideDisables;

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
        foreach (Behaviour comp in GetComponents<Behaviour>())
        {
            if (comp == deathEffect)
            {
                continue;
            }
            comp.enabled = false;
        }
        foreach (GameObject child in outsideDisables)
        {
            if (child.TryGetComponent<SpriteRenderer>(out SpriteRenderer ospr))
            {
                ospr.enabled = false;
            }
            else if (child.TryGetComponent<ValueBar>(out ValueBar vb))
            {
                child.transform.position = new Vector3(999, 999, 999);
            }
        }
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spr))
        {
            spr.enabled = false;
        }
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D move))
        {
            move.linearVelocity = Vector2.zero;
        }
        Destroy(gameObject, 2f);
    }

}
