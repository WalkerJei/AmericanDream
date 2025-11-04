using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    // 슬롯의 총 개수
    [SerializeField] byte slotCount;

    // 연결된 인벤토리 UI
    [SerializeField] InventoryUI inventoryUI;
    /// <summary> 아이템 목록 </summary>
    [SerializeField] Item[] items;

    private void Awake()
    {
        items = new Item[slotCount];

    }

    /// <summary> 인덱스가 수용 범위 내에 있는 지 검사 </summary>
    private bool IsValidIndex(int id)
    {
        return id >= 0 && id < slotCount;
    }

    /// <summary> 앞에서부터 빈 슬롯 인덱스 탐색 </summary>
    private int FindEmptySlotId(int startId = 0)
    {
        for(int i = startId; i < slotCount; i++)
            if(items[i] == null)
                return i;
        return -1;
    }

    ///<summary> 해당 슬롯이 아이템을 가지고 있는 여부 </summary>
    public bool HasItem(int id)
    {
        return IsValidIndex(id) && items[id] != null;
    }

    /// <summary> 해당 슬롯의 아이템 정보 리턴 </summary>
    public ItemData GetItemData(int id)
    {
        if (IsValidIndex(id)) return null;
        if(items[id] == null) return null;

        return items[id].ItemData;
    }

    ///<summary> 해당 슬롯의 아이템 이름 리턴 </summary>
    public string GetItemName(int id)
    {
        if (IsValidIndex(id)) return "";
        if (items[id] == null) return "";

        return items[id].ItemData.ItemName;
    }
}
