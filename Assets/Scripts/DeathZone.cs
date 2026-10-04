using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IHandlesDeath>(out IHandlesDeath deathhadler))
        {
            deathhadler.Die(true);
        }
    }
}
