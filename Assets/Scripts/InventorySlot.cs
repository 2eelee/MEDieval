using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public bool isEmpty => item == null;

    public InventorySlot()
    {
        item = null;
    }

    public bool AddItem(Item newItem, int maxStackSize)
    {
        if (isEmpty)
        {
            item = newItem;
            return true;
        }
        else if (item.itemID == newItem.itemID && item.quantity < maxStackSize)
        {
            int addAmount = Mathf.Min(newItem.quantity, maxStackSize - item.quantity);
            item.quantity += addAmount;
            return addAmount == newItem.quantity;
        }
        return false;
    }

    public void RemoveItem()
    {
        item = null;
    }
}
