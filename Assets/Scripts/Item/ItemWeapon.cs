using UnityEngine;

public class ItemWeapon : Item
{
    ItemWeaponData itemWeaponData { get; set; }
    
    public ItemWeapon(ItemWeaponData data) : base(data)
    {
        itemWeaponData = data;
    }
}
