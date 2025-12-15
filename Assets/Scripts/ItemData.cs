using UnityEngine;

[CreateAssetMenu(
    fileName = "NewItemData",
    menuName = "Inventory/Item Data",
    order = 0)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int maxStack = 99;           // 최대 중첩 개수
    public bool isStackable = true;     // 중첩 가능 여부
    
    [Range(1, 99)]
    public int maxQuantity = 99;        // 최대 99개
}

