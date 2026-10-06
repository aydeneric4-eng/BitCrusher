using System;
using Unity.VisualScripting;
using UnityEngine;

public class HealthTracker : MonoBehaviour, IDamagable, IHasTeam
{
    [SerializeField] PlayerTeams playerTeam = PlayerTeams.Player;
    public PlayerTeams PlayerTeam
    {
        get => playerTeam;
        set => playerTeam = value;
    }

    [SerializeField] AudioClip hurtSFX;

    [SerializeField] ValueBar healthBar = null;
    [SerializeField] float maxHealth = 100;
    private float currentHealth;
    private bool isInvincible = false;
    private float invincibilityStartTime;
    private float invincibilityDuration;

    public event Action healthAtZero;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (healthBar)
            healthBar.Setup(currentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        if (healthBar)
            healthBar.UpdateValue(currentHealth);
    }

    public void Damage(float damage)
    {
        if (isInvincible)
            return;

        if (hurtSFX)
            GameManager.Instance.audioManager.PlaySFX(hurtSFX);

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar)
            healthBar.UpdateValue(currentHealth);

        if (currentHealth <= 0)
        {
            healthAtZero?.Invoke();
        }
    }

    public void MakeInvincible(float duration)
    {
        isInvincible = true;
        invincibilityDuration = duration;
        invincibilityStartTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (isInvincible && CustomUtilities.HasTimeElapsed(invincibilityStartTime, invincibilityDuration))
        {
            isInvincible = false;
        }
    }
}
