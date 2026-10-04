using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DeathHandler))]
public class HealthTracker : MonoBehaviour, IDamagable, IHasTeam
{
    [SerializeField] PlayerTeams playerTeam = PlayerTeams.Player;
    [SerializeField] ValueBar healthBar = null;
    
    public PlayerTeams PlayerTeam
    {
        get => playerTeam;
        set => playerTeam = value;
    }

    [SerializeField] float maxHealth = 100;
    private DeathHandler deathHandler;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        deathHandler = GetComponent<DeathHandler>();
    }

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.Setup(currentHealth, maxHealth);
        }

    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.UpdateValue(currentHealth);

        if (currentHealth <= 0)
        {
            deathHandler.KillThisObject();
        }
    }
}
