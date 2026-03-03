using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [Tooltip("아이템 이미지")]
    [SerializeField] Image itemImage;

    [Tooltip("아이템 개수 표시")]
    [SerializeField] TextMeshProUGUI itemStackText;

    [Tooltip("슬롯에 커서를 올리면 나타나는 하이라이트 이미지")]
    [SerializeField] Image hightlightImage;

    ///<summary> 슬롯의 인덱스 </summary>
    [SerializeField] int id;
    public int Id { get; private set; }

    /// <summary> 접근 가능한 슬롯 여부 </summary>
    bool hasItem => itemImage.sprite != null;
    public bool HasItem
    {
        get { return hasItem; }
    }

    private InventoryUI inventoryUI;

    /// <summary> 슬롯 오브젝트 </summary>
    private RectTransform slotRect;
    public RectTransform SlotRect
    {
        get { return slotRect; }
    }
    
    private RectTransform imageRect;
    public RectTransform ImageRect
    {
        get { return imageRect; }
    }

    private RectTransform highlightRect;

    private GameObject imageGameObject;
    private GameObject textGameObject;
    private GameObject hightlightGameObject;

    private Image slotImage;

    private void ShowImage() => imageGameObject.SetActive(true);
    private void HideImage() => imageGameObject.SetActive(false);

    private void ShowText() => textGameObject.SetActive(true);
    private void HideText() => textGameObject.SetActive(false);

    public void SetSlotID(int id) => Id = id;

    /// <summary> 타 슬롯과 아이템 아이콘 교환 </summary>
    public void SwapMoveImage(ItemSlotUI other)
    {
        if(other ==null) return;
        // 자기 자신과 교환하는 것은 불가능하다.
        else if(other ==this) return;

        var temp = itemImage.sprite;

        // 대상에 아이템이 있다면 위치를 교환한다
        if (other.HasItem) SetItem(other.itemImage.sprite);
        // 대상에 아이템이 없다면 이동한다.
        else RemoveItem();
        
        other.SetItem(temp);
    }

    /// <summary> 슬롯에 아이템 등록 </summary>
    public void SetItem(Sprite itemSprite)
    {
        if (itemSprite != null)
        {
            itemImage.sprite = itemSprite;
            ShowImage();
        }
        else
            RemoveItem();
    }

    /// <summary> 슬롯에서 아이템 제거 </summary>
    public void RemoveItem()
    {
        itemImage.sprite = null;
        HideImage();
        HideText();
    }

    /// <summary> 아이템 개수 텍스트 설정 (stack이 1 이하이면 미표시) </summary>
    public void SetItemStack(byte stack)
    {
        if (HasItem && stack > 1)
            ShowText();
        else
            HideText();

        itemStackText.text = stack.ToString();
    }

    public void SetHighlightOnTop(bool value)
    {

    }
}
