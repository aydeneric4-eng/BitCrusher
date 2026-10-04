using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(BulletShooter))]
public class PlayerShooter : MonoBehaviour
{
    BulletShooter bulletShooter;
    private bool isShooting = false;

    private void Awake()
    {
        bulletShooter = GetComponent<BulletShooter>();
    }
    private void FixedUpdate()
    {
        if (isShooting)
        {
            bulletShooter.ShootBullet();
        }
    }

    public void ShootInput(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }
}
