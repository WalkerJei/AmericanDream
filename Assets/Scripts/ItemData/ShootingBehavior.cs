using UnityEngine;

public abstract class ShootingBehavior : ScriptableObject
{
    public abstract void Shoot(Transform firePoint);
}
