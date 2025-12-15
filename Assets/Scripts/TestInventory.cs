using UnityEngine;

public class TestInventory : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private ItemData testItem1;
    [SerializeField] private ItemData testItem2;
    
    private void Start()
    {
        inventory.AddItem(testItem1, 50);
        inventory.AddItem(testItem2, 30);
        inventoryUI.RefreshUI();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(testItem1, 100);
            inventoryUI.RefreshUI();
            Debug.Log("아이템 추가!");
        }
    }
}

