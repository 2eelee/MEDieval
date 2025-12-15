using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI pageText;
    [SerializeField] private int slotsPerPage = 6;
    
    private InventorySlot[] slots;
    
    private void Start()
    {
        InitializeSlots();
        
        RefreshUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
    {
        inventory.NextPage();
        RefreshUI();
    }
        nextButton.onClick.AddListener(() => 
        {
            inventory.NextPage();
            RefreshUI();
        });
    }
    
    // ============ 슬롯 초기화 (6개 생성) ============
    private void InitializeSlots()
    {
        slots = new InventorySlot[slotsPerPage];
        
        for (int i = 0; i < slotsPerPage; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotContainer);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slots[i] = slot;
        }
    }
    
    // ============ UI 갱신 ============
    public void RefreshUI()
    {
        var pageItems = inventory.GetCurrentPageItems();
        
        // 슬롯 업데이트
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < pageItems.Count)
            {
                slots[i].SetItem(pageItems[i]);
            }
            else
            {
                slots[i].Clear();
            }
        }
        
        // 페이지 텍스트 (1/2, 2/2 식)
        int maxPage = Mathf.CeilToInt((float)inventory.Items.Count / slotsPerPage);
        if (maxPage == 0) maxPage = 1;
        
        pageText.text = $"{inventory.CurrentPage + 1}/{maxPage}";
        
        // 버튼 활성화 (1페이지만 있으면 비활성)
        nextButton.interactable = maxPage > 1;
    }
}