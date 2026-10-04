using UnityEngine;

[RequireComponent(typeof(DeathHandler))]
public class HealthTracker : MonoBehaviour, IDamagable, IHasTeam
{
    [SerializeField] PlayerTeams playerTeam = PlayerTeams.Player;
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

    public void Damage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            deathHandler.KillThisObject();
        }
    }
}
