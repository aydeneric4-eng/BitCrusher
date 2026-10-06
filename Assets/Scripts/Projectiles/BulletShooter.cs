using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] Transform muzzleTransform;
    [SerializeField] Bullet bulletPrefab;

    [SerializeField] float rateOfFire = 1f;
    private float lastFiredTime = -999f;

    [SerializeField] AudioClip shootSFX;
    public void ShootBullet()
    {
        if (!CustomUtilities.HasTimeElapsed(lastFiredTime,rateOfFire))
        {
            return;
        }

        lastFiredTime = Time.time;
        Bullet newBullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
        if (shootSFX)
        {
            GameManager.Instance.audioManager.PlaySFX(shootSFX);
        }
    }
}
