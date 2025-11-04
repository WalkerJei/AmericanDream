using UnityEngine;

// 무기 아이템
[CreateAssetMenu(fileName = "ItemWeapon", menuName = "Scriptable Objects/ItemWeapon")]
public class ItemWeaponData : ItemData
{
    // 사용하는 총알 종류
    public enum UsingBullet { coroot, soft, spenso, benjamin, govern, gag }
    [SerializeField] UsingBullet usingBullet;
    // 현재 장전된 총알 수
    [SerializeField] byte bulletCnt;
    public byte BulletCnt
    {
        get { return bulletCnt; }
        set { bulletCnt = (byte)Mathf.Clamp(value, 0, maxBulletCnt); }
    }
    // 최대 장전 총알 수
    [SerializeField] byte maxBulletCnt;
    // 재장전에 걸리는 시간
    [SerializeField] float reloadTime;
    // 차탄 발사에 걸리는 시간
    [SerializeField] float fireRate;
    // 사거리
    [SerializeField] byte range;
    // 발사음
    [SerializeField] AudioClip fireSound;

    public ShootingBehavior shootingBehavior;

    public override Item CreateItem()
    {
        return new ItemWeapon(this);
    }
}
