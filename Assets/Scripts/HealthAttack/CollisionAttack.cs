using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionAttack : MonoBehaviour, IHasTeam
{
    [SerializeField] bool active = true;
    [SerializeField] PlayerTeams playerTeam = PlayerTeams.Enemy;
    public PlayerTeams PlayerTeam
    {
        get => playerTeam;
        set => playerTeam = value;
    }

    [SerializeField] float damage = 50f;
    [SerializeField] float selfKnockback = 20f;
    [SerializeField] bool alwaysGiveSelfKB = false;

    private IReceivesKnockback selfKBReceiver;
    private void Awake()
    {
        selfKBReceiver = GetComponent<IReceivesKnockback>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active)
            return;

        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damageHandler))
        {
            if (damageHandler is IHasTeam team && team.PlayerTeam == playerTeam)
            {
                return;
            }
            damageHandler.Damage(damage);
        }
        if (selfKBReceiver != null && (!collision.gameObject.TryGetComponent<IGivesKnockback>(out IGivesKnockback KBGiver) || alwaysGiveSelfKB))
        {
            selfKBReceiver.ReceiveKnockback(CustomUtilities.GetAverageCollisionNormal(collision) * selfKnockback);
        }
    }
}
