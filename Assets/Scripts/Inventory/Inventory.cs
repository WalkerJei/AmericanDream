using UnityEngine;

public class Inventory : MonoBehaviour
{
    /// <summary> 인벤토리 백 슬롯의 총 개수 </summary>
    [SerializeField] byte bagSlotCount;
    /// <summary> 인벤토리 손 슬롯의 총 개수 </summary>
    [SerializeField] byte handSlotCount;
    /// <summary> 인벤토리 갑옷 슬롯의 총 개수 </summary>
    [SerializeField] byte armorSlotCount;
    
    /// <summary> 연결된 인벤토리 </summary>
    [SerializeField] InventoryUI inventoryUI;
    /// <summary> 백 안의 아이템 목록 </summary>
    [SerializeField] Item[] itemsBag;
    /// <summary> 손 안의 아이템 목록 </summary>
    [SerializeField] Item[] itemsHand;
    /// <summary> 착용한 갑옷 목록 </summary>
    [SerializeField] Item[] itemsArmor;

    /// <summary> 게임머니 </summary>
    ushort dollar;
    public ushort Dollar
    {
        get { return dollar; }
        set { dollar = value; }
    }

    public Item[] Items
    {
        get { return itemsBag; }
        set { itemsBag = value; }
    }

    // Unity Event
    #region
    private void Awake()
    {
        itemsBag = new Item[bagSlotCount];
        inventoryUI.SetInventoryReference(GetComponentInChildren<InventoryBag>());
    }
    #endregion

    
    /// <summary> 인덱스가 수용 범위 내에 있는 지 검사 </summary>
    private bool IsValidIndex(int id)
    {
        return id >= 0 && id < bagSlotCount;
    }

    /// <summary> 앞에서부터 빈 슬롯 인덱스 탐색 </summary>
    public int FindEmptySlotId(int startId = 0)
    {
        for(int i = startId; i < bagSlotCount; i++)
            if(itemsBag[i] == null)
                return i;
        return -1;
    }

    ///<summary> 앞에서부터 개수 여유가 있는 CanOverlap 아이템의 슬롯 인덱스 탐색 </summary>
    public int FindCanOverlapItemSlotId(ItemData itemData, int startId = 0)
    {
        for (int i = startId; i < bagSlotCount; i++)
        {
            var current = itemsBag[i];
            if(current == null) continue;

            // 아이템 종류의 일치 여부와 개수 여유 확인
            if(current.ItemData == itemData && current is Item item)
            {
                if(!item.IsFullStack)
                    return i;
            }
        }

        return -1;
    }

    ///<summary> 해당하는 인덱스의 슬롯 상태 및 UI 갱신 </summary>
    public void UpdateSlot(int id)
    {
        if (IsValidIndex(id)) return;
        Item item = itemsBag[id];

        // 아이템이 슬롯 안에 있을 경우
        if (item != null)
        {
            inventoryUI.SetItemIcon(id, item.ItemData.ItemImage);

            // 셀 수 있는 아이템일 경우
            if (item.ItemData.ItemCanOverlap == true)
            {
                // 수량이 0이면 아이템을 슬롯에서 제거
                if (item.IsEmptyStack)
                {
                    itemsBag[id] = null;
                    RemoveIcon();
                    return;
                }
                // 수량이 0이 아니면 텍스트로 표시
                else
                    inventoryUI.SetItemStackText(id, item.Stack);
            }
            // 셀 수 없는 아이템인 경우 수량 텍스트 제거
            else
                inventoryUI.HideItemStackText(id);
        }
        // 빈 슬롯인 경우 수량 텍스트 제거
        else
            RemoveIcon();

        void RemoveIcon()
        {
            inventoryUI.RemoveItem(id);
            inventoryUI.HideItemStackText(id);
        }
    }

    ///<summary> 해당 슬롯이 아이템을 가지고 있는 여부 </summary>
    public bool HasItem(int id)
    {
        return IsValidIndex(id) && itemsBag[id] != null;
    }

    /// <summary> 해당 슬롯의 아이템 개수 리턴 </summary>
    public byte GetSlotItemStack(int id)
    {
        if (!IsValidIndex(id)) return 0;
        if(itemsBag[id] == null) return 0;

        Item itemInfo = itemsBag[id] as Item;
        if (itemInfo.ItemData.ItemCanOverlap == false)
            return 1;

        return itemInfo.Stack;
    }

    /// <summary> 해당 슬롯의 아이템 정보 리턴 </summary>
    public ItemData GetItemData(int id)
    {
        if (IsValidIndex(id)) return null;
        if(itemsBag[id] == null) return null;

        return itemsBag[id].ItemData;
    }

    ///<summary> 해당 슬롯의 아이템 이름 리턴 </summary>
    public string GetItemName(int id)
    {
        if (IsValidIndex(id)) return "";
        if (itemsBag[id] == null) return "";

        return itemsBag[id].ItemData.ItemName;
    }

    public int AddItem(ItemData itemData, byte stack = 1)
    {
        int id;

        // 수량이 있는 아이템인 경우
        if (itemData.ItemCanOverlap == true)
        {
            bool findNextCanOverlap = true;
            id = -1;

            while (stack > 0)
            {
                // 이미 해당 아이템이 인벤토리 내에 존재하고 개수 여유가 있는 지 검사
                if (findNextCanOverlap)
                {
                    id = FindCanOverlapItemSlotId(itemData, id + 1);

                    // 개수 여유가 있는 슬롯이 없으면 빈 슬롯부터 탐색 시작
                    if (id == -1)
                        findNextCanOverlap = false;
                    else
                    {
                        Item item = Items[id] as Item;
                        stack = item.AddStackAndGetExcess(stack);

                        UpdateSlot(id);
                    }
                }
                // 빈 슬롯 탐색
                else
                {
                    id = FindEmptySlotId(id + 1);

                    // 빈 슬롯이 없으면 종료
                    if (id == -1)
                        break;
                    else
                    {
                        // 새로운 아이템 생성
                        Item item = itemData.CreateItem() as Item;
                        item.SetStack(stack);

                        // 슬롯에 추가
                        Items[id] = item;

                        // 남은 개수 계산
                        stack = (byte)((stack > itemData.MaxStack) ? (stack - itemData.MaxStack) : 0);

                        UpdateSlot(id);
                    }
                }
            }
        }
        // 수량이 없는 아이템인 경우
        else
        {
            // 1개만 넣는 경우
            if (stack == 1)
            {
                id = FindEmptySlotId();
                if (id != -1)
                {
                    // 아이템 생성 후 슬롯에 추가
                    Items[id] = itemData.CreateItem();
                    stack = 0;

                    UpdateSlot(id);
                }
            }

            // 2개 이상의 수량 없는 아이템을 동시에 추가하는 경우
            id = -1;
            for (; stack > 0; stack--)
            {
                // 아이템 넣은 인덱스의 다음 인덱스부터 슬롯 탐색
                id = FindEmptySlotId(id + 1);

                // 다 넣지 못하면 루프 종료
                if (id == -1)
                    break;

                // 아이템을 생성해 슬롯에 추가
                Items[id] = itemData.CreateItem();

                UpdateSlot(id);
            }
        }
        return stack;
    }
}
