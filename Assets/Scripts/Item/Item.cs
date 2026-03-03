using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private ItemData itemData;
    public ItemData ItemData
    {
        get { return itemData; }
        private set { itemData = value; }
    }

    /// <summary> 인벤토리 슬롯 한 칸에 쌓은 아이템의 개수 </summary>
    byte stack;
    public byte Stack
    {
        get { return stack; }
        protected set { stack = (byte)Mathf.Clamp(value, 0, MaxStack); }
    }

    /// <summary> 인벤토리 슬롯 한 칸에 쌓을 수 있는 최대 아이템의 개수 </summary>
    byte maxStack;
    public byte MaxStack
    {
        get { return ItemData.MaxStack; }
    }

    /// <summary> 인벤토리 슬롯 하나의 아이템 스택이 가득 찼는지 여부 </summary>
    bool isFullStack => Stack >= itemData.MaxStack;
    public bool IsFullStack
    {
        get { return isFullStack; }
    }

    /// <summary> 슬롯 하나의 아이템 스택이 텅 비었는 지 여부 </summary>
    bool isEmptyStack => Stack <= 0; 
    public bool IsEmptyStack
    {
        get { return  isEmptyStack; }
    }


    public Item(ItemData itemData) => ItemData = itemData;

    /// <summary> 개수 지정 </summary>
    public void SetStack(byte stack)
    {
        Stack = (byte)Mathf.Clamp(stack, 0, MaxStack);
    }

    /// <summary> 개수 추가와 최대치 초과량 반환 </summary>
    public byte AddStackAndGetExcess(byte stack)
    {
        byte nextStack = (byte)(Stack + stack);
        SetStack(nextStack);

        return (byte)((nextStack > MaxStack) ? (nextStack - MaxStack) : 0);
    }
}
