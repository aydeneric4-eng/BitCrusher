using UnityEngine;

[RequireComponent(typeof(BulletShooter))]
public class EnemyShootingManager : MonoBehaviour
{
    [SerializeField] LayerMask terrainMask;
    [SerializeField] float minShootDistance = 5f;
    private Transform targetTransform;
    private BulletShooter bulletShooter;

    private void Awake()
    {
        bulletShooter = GetComponent<BulletShooter>();
    }

    private void FixedUpdate()
    {
        if (!targetTransform)
        {
            targetTransform = GameManager.Instance.playerIntance.transform;
        }

        if ((transform.position - targetTransform.position).magnitude > minShootDistance)
        {
            //Debug.Log("Too far");
            return;
        }

        RaycastHit2D hitData = Physics2D.Linecast(transform.position, targetTransform.position, terrainMask);
        if (hitData)
        {
            //Debug.Log("Terrain in way");
            return;
        }
        //Debug.Log("good to shoot0");
        bulletShooter.ShootBullet();
    }
}
