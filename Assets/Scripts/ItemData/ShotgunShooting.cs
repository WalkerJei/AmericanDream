using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunShooting", menuName = "Scriptable Objects/ShootingBehavior/Shotgun")]
public class ShotgunShooting : ShootingBehavior
{
    [SerializeField] byte pelletCount;
    [SerializeField] float spreadAngle;
    [SerializeField] float pelletSpeed;

    public override void Shoot(Transform firePoint)
    {

    }
}
