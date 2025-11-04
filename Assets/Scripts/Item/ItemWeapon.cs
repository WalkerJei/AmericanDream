using UnityEngine;

public class ItemWeapon : Item
{
    public ItemWeaponData itemWeaponData { get; private set; }
    
    public ItemWeapon(ItemWeaponData data) : base(data)
    {
        itemWeaponData = data;
    }
}
