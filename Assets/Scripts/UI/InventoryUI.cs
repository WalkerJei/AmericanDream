using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{   
    private Inventory inventory;
    private List<ItemSlotUI> slotUIList = new List<ItemSlotUI>();

    [Header("연결 오브젝트")]
    // 슬롯들이 위치하는 영역
    [SerializeField] RectTransform slotArea;
    // 슬롯 프리팹
    [SerializeField] GameObject slotPrefab;
    // 배치할 슬롯의 개수
    [SerializeField] byte slotCount;

    /// <summary> 연결된 인벤토리 </summary>
       
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastList;

    // 현재 드래그를 시작한 슬롯
    private ItemSlotUI beginDragSlot;
    // 드래그를 시작한 슬롯의 위치
    private Transform beginDragItemTransform;


    // 드래그 시작 시 슬롯의 위치
    private Vector2 beginDragItemPoint;
    // 드래그 시작 시 커서의 위치
    private Vector2 beginDragCursorPoint;
    private int beginDragSlotSiblingId;

    // 인벤토리 창 활성화 여부
    [SerializeField] GameObject inventoryImage;
    bool activeInventory = false;
    public bool ActiveInventory
    {
        get { return activeInventory; }
        set { activeInventory = value; }
    }

    InputAction inventoryAction;


    private void Awake()
    {
       InitialSlots();
    }

    private void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        inventoryImage.SetActive(activeInventory);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        OnToggleInventoryUI();
    }

    // 동적으로 인벤토리에 슬롯 생성
    private void InitialSlots()
    {
        slotPrefab.TryGetComponent(out ItemSlotUI itemSlot);
        if(itemSlot == null)
            slotPrefab.AddComponent<ItemSlotUI>();
        
        slotPrefab.SetActive(false);

        slotUIList = new List<ItemSlotUI>(slotCount);

        for (int i = 0; i < slotCount; i++) 
        {
            int slotId = i + 1;
         
            var slotRT = CloneSlot();
            slotRT.gameObject.SetActive(true);

            var slotUI = slotRT.GetComponent<ItemSlotUI>();
            slotUI.SetSlotID(slotId);
            slotUIList.Add(slotUI);
        }


        if (slotPrefab.scene.rootCount != 0)
            Destroy(slotPrefab);

        RectTransform CloneSlot()
        {
            GameObject slotGo = Instantiate(slotPrefab);
            RectTransform rt = slotGo.GetComponent<RectTransform>();
            
            // 슬롯을 부모 오브젝트인 ItemBag에 배치
            // false로 로컬 스케일을 유지해 비정상적인 크기로 커지는 것을 방지
            rt.SetParent(slotArea, false);

            return rt;
        }
    }

    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        raycastList.Clear();

        raycaster.Raycast(pointerEventData, raycastList);

        if (raycastList.Count == 0)
            return null;
        return raycastList[0].gameObject.GetComponent<T>();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            pointerEventData.position = eventData.pressPosition;
            Debug.Log(pointerEventData.position);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            beginDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

            if (beginDragSlot != null && beginDragSlot.HasItem)
            {
                // 위치 기억, 참조 등록
                beginDragItemTransform = beginDragSlot.ImageRect.transform;
                beginDragItemPoint = beginDragItemTransform.position;
                beginDragCursorPoint = eventData.pressPosition;

                // 맨 위에 보이게 한다
                beginDragSlotSiblingId = beginDragSlot.transform.GetSiblingIndex();
                beginDragSlot.transform.SetAsLastSibling();

                // 해당 슬롯의 하이라이트 이미지를 아이템 이미지보다 뒤에 위치시키기
                beginDragSlot.SetHighlightOnTop(false);
            }
            else
                beginDragSlot = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(beginDragSlot == null) return;
         
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            // 아이템의 위치 이동
            beginDragItemTransform.position =
                beginDragItemPoint + (eventData.pointerCurrentRaycast.screenPosition - beginDragCursorPoint);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(beginDragSlot != null)
            {
                // 위치 복원
                beginDragItemTransform.position = beginDragItemPoint;
                // 드래그 완료 처리
                beginDragSlot = null;
                beginDragItemTransform = null;
            }
        }
    }
    
    // 토글로 인벤토리 열고 닫기
    public void OnToggleInventoryUI()
    {
        if (inventoryAction.WasPressedThisFrame())
        {
            Debug.Log(inventoryAction);
            ActiveInventory = !ActiveInventory;
            inventoryImage.SetActive(ActiveInventory);
        }
    }
}
