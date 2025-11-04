using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private ItemData itemData;
    public ItemData ItemData
    {
        get { return itemData; }
        private set { itemData = value; }
    }

    public Item(ItemData itemData) => ItemData = itemData;
}
