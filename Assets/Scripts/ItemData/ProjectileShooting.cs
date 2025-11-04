using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShooting", menuName = "Scriptable Objects/ShootingBehavior/Projectile")]
public class ProjectileShooting : ShootingBehavior
{
    // 발사할 총알
    [SerializeField] GameObject projectilePrefab;
    // 탄속
    [SerializeField] float projectileSpeed;

    // 발사
    public override void Shoot(Transform firePoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        if(projectile.TryGetComponent(out Rigidbody2D rigidbody2D))
            rigidbody2D.linearVelocity = firePoint.forward * projectileSpeed;
    }
}
