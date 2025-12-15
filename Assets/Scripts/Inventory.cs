using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotPerPage = 6;
    private List<Item> items = new List<Item>();
    private int currentPage = 0;
    
    public int CurrentPage => currentPage;
    public int MaxPage => Mathf.CeilToInt((float)items.Count / slotPerPage);
    public List<Item> Items => items;
    
    // ============ 아이템 추가 ============
    public bool AddItem(ItemData itemData, int quantity = 1)
    {
        // 중첩 가능한 아이템은 먼저 기존 슬롯에 추가
        if (itemData.isStackable)
        {
            foreach (Item item in items)
            {
                if (item.data == itemData && item.quantity < itemData.maxStack)
                {
                    int addAmount = Mathf.Min(quantity, itemData.maxStack - item.quantity);
                    item.quantity += addAmount;
                    
                    // 남은 수량이 있으면 재귀 호출
                    if (quantity > addAmount)
                    {
                        return AddItem(itemData, quantity - addAmount);
                    }
                    return true;
                }
            }
        }
        
        // 새로운 슬롯 추가
        items.Add(new Item(itemData, quantity));
        
        // 페이지 자동 조정
        int newMaxPage = Mathf.CeilToInt((float)items.Count / slotPerPage);
        if (currentPage >= newMaxPage)
        {
            currentPage = newMaxPage - 1;
        }
        
        return true;
    }
    
    // ============ 아이템 제거 ============
    public bool RemoveItem(int index, int quantity = 1)
    {
        if (index < 0 || index >= items.Count) return false;
        
        items[index].quantity -= quantity;
        if (items[index].quantity <= 0)
        {
            items.RemoveAt(index);
        }
        
        return true;
    }
    
    // ============ 현재 페이지 아이템 가져오기 ============
    public List<Item> GetCurrentPageItems()
    {
        List<Item> pageItems = new List<Item>();
        int startIndex = currentPage * slotPerPage;
        int endIndex = Mathf.Min(startIndex + slotPerPage, items.Count);
        
        for (int i = startIndex; i < endIndex; i++)
        {
            pageItems.Add(items[i]);
        }
        
        return pageItems;
    }
    
    // ============ 페이지 이동 ============
    public void NextPage()
    {
        int maxPage = Mathf.CeilToInt((float)items.Count / slotPerPage);
        currentPage++;
        
        if (currentPage >= maxPage)
        {
            currentPage = 0;  // 순환
        }
    }    
    public void SetPage(int page)
    {
        int maxPage = Mathf.CeilToInt((float)items.Count / slotPerPage);
        currentPage = Mathf.Clamp(page, 0, maxPage - 1);
    }
}