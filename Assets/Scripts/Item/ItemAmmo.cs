using UnityEngine;

public class ItemAmmo : Item
{
    ItemAmmoData itemAmmoData { get; set; }

    public ItemAmmo(ItemAmmoData data): base(data)
    {
        itemAmmoData = data;
    }
}
