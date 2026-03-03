using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    InputAction inventoryAction;
    
    // 인벤토리 창 활성화 여부
    [SerializeField] GameObject inventoryImage;
    bool activeInventory = false;
    public bool ActiveInventory
    {
        get { return activeInventory; }
        set { activeInventory = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        inventoryImage.SetActive(activeInventory);
    }

    // Update is called once per frame
    void Update()
    {
        OnToggleInventoryUI();
    }

    // 토글로 인벤토리 열고 닫기
    public void OnToggleInventoryUI()
    {
        if (inventoryAction.WasPressedThisFrame())
        {
            ActiveInventory = !ActiveInventory;
            inventoryImage.SetActive(ActiveInventory);
        }
    }
}
