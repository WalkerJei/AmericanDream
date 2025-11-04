using UnityEngine;

public class ObjectItem : MonoBehaviour, IObjectItem
{
    // 오브젝트가 가지는 아이템 정보
    [SerializeField] ItemData itemObjectInfo;
    public ItemData ItemObjectInfo
    {
        get { return itemObjectInfo; }
        set { itemObjectInfo = value; }
    }

    // 아이템 오브젝트의 이미지
    [SerializeField] SpriteRenderer itemObjectImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 미리 만든 아이템 데이터에 저장된 이미지를 가져와서 itemObjectIamge에 배치한다
        itemObjectImage.sprite = ItemObjectInfo.ItemImage;
    }

    public ItemData ContactItem()
    {
        return this.itemObjectInfo;
    }
}
