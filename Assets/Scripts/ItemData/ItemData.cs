using UnityEngine;

[System.Flags]
public enum ItemType
{
    /// <summary>
    /// None은 아이템 습득 키를 눌러도 인벤토리에 아이템이 들어오지 않는다.
    /// </summary>
    None = 0b0,

    // 무기로 리볼버, 리피터, 라이플, 산탄총이 있다
    Weapon_Revolver = 0b1,
    Weapon_Repeater = 0b10,
    Weapon_Rifle = 0b100,
    Weapon_Shotgun = 0b100,

    // 탄약
    Ammo = 0b1000,

    // 방어구로 머리, 상의, 하의, 발이 있다
    Armor_Head = 0b10000,
    Armor_Top = 0b100000,
    Armor_Bottom = 0b1000000,
    Armor_Foot = 0b10000000,
            
    // 도구
    Tool = 0b100000000,
    // 음식
    Food = 0b1000000000,
    // 기타 아이템
    other = 0b10000000000
}

// 아이템 데이터의 기반
public abstract class ItemData : ScriptableObject
{
    // 아이템의 ID
    [Header("아이템의 ID로 중복은 불가능하다.")]
    [SerializeField] byte itemID;
    public byte ItemID
    {
        get { return itemID; }
    }

    [Header("아이템의 중첩 가능 여부")]
    [SerializeField] bool itemCanOverlap;
    public bool ItemCanOverlap
    {
        get { return itemCanOverlap; }
    }

    [Header("아이템의 상호작용 가능 여부")]
    [SerializeField] bool itemCanInteract;
    public bool ItemCanInteract
    {
        get { return itemCanInteract; }
    }

    [Header("아이템 사용 시 사라지는 여부")]
    [SerializeField] bool itemCanConsumable;
    public bool ItemCanConsumable
    {
        get { return itemCanConsumable; }
    }

    [Header("아이템 타입")]
    [SerializeField] ItemType itemType;
    public ItemType ItemType
    {
        get { return itemType; }
    }

    // 아이템 이름
    [SerializeField] string itemName;
    public string ItemName
    {
        get { return name; }
    }

    // 구매가
    [SerializeField] ushort buyPrice;
    // 판매가
    [SerializeField] ushort sellPrice;

    // 인벤토리 한 칸에 쌓을 수 있는 아이템의 최대 양
    [SerializeField] byte maxStack;
    public byte MaxStack
    {
        get { return maxStack; }
    }

    // 아이템 설명
    [SerializeField] string itemExplanation;

    // 아이템 이미지
    [SerializeField] Sprite itemImage;
    public Sprite ItemImage
    {
        get { return itemImage; }
        set { itemImage = value; }
    }

    // 타입에 맞는 새로운 아이템 생성
    public abstract Item CreateItem();
}
