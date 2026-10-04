using UnityEngine;

public interface IDamagable
{
    void Damage(float damage);
    void MakeInvincible(float duration);
}
