using UnityEngine;

// 탄약 아이템
[CreateAssetMenu(fileName = "ItemAmmo", menuName = "Scriptable Objects/ItemAmmo")]
public class ItemAmmoData : ItemData
{
    // 사용하는 총알 종류
    public enum TypeBullet { coroot, soft, spenso, benjamin, govern, gag }
    [SerializeField] TypeBullet typeBullet;

    public override Item CreateItem()
    {
        return new ItemAmmo(this);
    }
}
