using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] Transform muzzleTransform;
    [SerializeField] Bullet bulletPrefab;

    [SerializeField] float rateOfFire = 1f;

    [SerializeField] bool isRandDelay = false;
    private float randDelay = 0f;
    private float lastFiredTime = -999f;

    [SerializeField] AudioClip shootSFX;
    private void Awake()
    {
        if (isRandDelay)
        {
            randDelay = UnityEngine.Random.Range(0f, 0.2f);
        }
    }

    public void ShootBullet()
    {
        if (!CustomUtilities.HasTimeElapsed(lastFiredTime,rateOfFire + randDelay))
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
