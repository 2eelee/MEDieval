using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image slotBackground;
    
    private Item currentItem;
    private Color originalColor;
    
    private void Awake()
    {
        originalColor = slotBackground.color;
    }
    
    // ============ 슬롯에 아이템 설정 ============
    public void SetItem(Item item)
    {
        currentItem = item;
        
        if (item != null && item.data != null)
        {
            itemIcon.sprite = item.data.icon;
            itemIcon.enabled = true;
            
            // 중첩 가능 + 개수 > 1 일때만 표시
            if (item.data.isStackable && item.quantity > 1)
            {
                quantityText.text = item.quantity.ToString();
                quantityText.enabled = true;
            }
            else
            {
                quantityText.enabled = false;
            }
        }
    }
    
    // ============ 슬롯 비우기 ============
    public void Clear()
    {
        currentItem = null;
        itemIcon.enabled = false;
        quantityText.enabled = false;
        slotBackground.color = originalColor;
    }
    
    // ============ 마우스 호버 효과 ============
    public void OnHover()
    {
        if (currentItem != null)
        {
            slotBackground.color = new Color(1, 1, 1, 0.5f);
        }
    }
    
    public void OnHoverExit()
    {
        slotBackground.color = originalColor;
    }
}