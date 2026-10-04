using System.Data.Common;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem.XR.Haptics;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(IReceivesKnockback))]
public class NaturalKnockback : MonoBehaviour
{
    [SerializeField] float selfKnockbackPower = 20f;
    [SerializeField] bool allwaysGiveSelfKnockback = false;

    private IReceivesKnockback selfKBReceiver;

    private void Awake()
    {
        selfKBReceiver = GetComponent<IReceivesKnockback>();
    }

    private Vector2 collisionAverageNormal;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool colliderCanGiveKnockback = collision.collider.gameObject.TryGetComponent<IGivesKnockback>(out IGivesKnockback colliderKBGiver);

        if (!(!colliderCanGiveKnockback || allwaysGiveSelfKnockback))
            return;
        //Debug.Log(colliderCanGiveKnockback);
        collisionAverageNormal = Vector2.zero;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            collisionAverageNormal += contact.normal;
        }
        collisionAverageNormal = collisionAverageNormal / collision.contactCount;
        selfKBReceiver.ReceiveKnockback(collisionAverageNormal * selfKnockbackPower);
    }
}
