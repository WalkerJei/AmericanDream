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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public ItemData ContactItem()
    {
        return null;
    }
}
