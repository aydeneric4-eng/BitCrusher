using UnityEngine;
using static CustomUtilities;

public class Bullet : MonoBehaviour, IHasTeam
{
    [SerializeField] PlayerTeams playerTeam = PlayerTeams.Player;
    public PlayerTeams PlayerTeam
    {
        get => playerTeam;
        set => playerTeam = value;
    }

    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float damage = 50f;
    [SerializeField] bool giveKnockback = true;
    [SerializeField] float knockbackPower = 20f;

    private Transform selfTransform;
    private SpriteRenderer selfRenderer;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
        selfRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitData = Physics2D.Linecast(selfTransform.position, selfTransform.position + selfTransform.TransformDirection(new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0)));
        if (hitData)
        {
            handleCollision(hitData);
        }

        selfTransform.position += selfTransform.TransformDirection(new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0));
    }

    private void handleCollision(RaycastHit2D hitData)
    {
        GameObject collidedObject = hitData.collider.gameObject;

        if (collidedObject.TryGetComponent<IReceivesKnockback>(out IReceivesKnockback KBReceiver) && giveKnockback)
        {
            KBReceiver.ReceiveKnockback(-hitData.normal * knockbackPower);
        }

        if (collidedObject.TryGetComponent<IDamagable>(out IDamagable damageHandler))
        {
            if (damageHandler is IHasTeam team && team.PlayerTeam == playerTeam)
            {
                return;
            }
            damageHandler.Damage(damage);
        }

        if (selfRenderer)
            selfRenderer.enabled = false;
        Destroy(this);
        Destroy(gameObject, 0.25f);
    }
}
