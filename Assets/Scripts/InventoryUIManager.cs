using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private Transform materialSlotContainer; 
    [SerializeField] private Transform potionSlotContainer; 
    [SerializeField] private GameObject slotPrefab;              
    [SerializeField] private Canvas canvas;
    [SerializeField] private Text materialPageText;              
    [SerializeField] private Text potionPageText;

    private Inventory materialInventory;
    private Inventory potionInventory;
    private GameObject tooltipPanel;
    private Text tooltipText;
    private float pageChangeSpeed = 0.05f;
    private bool isChangingPage = false;

    void Start()
    {
        materialInventory = new Inventory(6);  
        potionInventory = new Inventory(5);   
  
        CreateTooltip();

        RefreshUI();

        TestAddItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isChangingPage)
        {
            StartCoroutine(ChangePageWithDelay(materialInventory, materialSlotContainer, materialPageText));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isChangingPage)
        {
            StartCoroutine(ChangePageWithDelay(potionInventory, potionSlotContainer, potionPageText));
        }
    }

    void CreateTooltip()
    {
        tooltipPanel = new GameObject("Tooltip");
        tooltipPanel.transform.SetParent(canvas.transform);
        Image img = tooltipPanel.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 0.9f);

        tooltipText = new GameObject("Text").AddComponent<Text>();
        tooltipText.transform.SetParent(tooltipPanel.transform);
        tooltipText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        tooltipText.text = "";
        tooltipText.color = Color.white;
        tooltipText.alignment = TextAnchor.MiddleCenter;

        RectTransform rect = tooltipPanel.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(150, 40);
        tooltipPanel.SetActive(false);
    }

    void RefreshUI()
    {
        RefreshInventoryUI(materialInventory, materialSlotContainer, materialPageText);
        RefreshInventoryUI(potionInventory, potionSlotContainer, potionPageText);
    }

    void RefreshInventoryUI(Inventory inventory, Transform slotContainer, Text pageText)
    {
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }
        var currentSlots = inventory.GetCurrentPageSlots();
        foreach (var slot in currentSlots)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotContainer);
            Image slotImage = slotObj.GetComponent<Image>();
            Text itemNameText = slotObj.transform.Find("ItemCount")?.GetComponent<Text>();

            if (slot.isEmpty)
            {
                slotImage.color = Color.gray;
                if (itemNameText) itemNameText.text = "";
            }
            else
            {
                slotImage.color = Color.white;
                slotImage.sprite = slot.item.icon;
                if (itemNameText && slot.item.quantity > 1)
                {
                    itemNameText.text = slot.item.quantity.ToString();
                }
            }
        }
        int totalPages = inventory.TotalPages > 0 ? inventory.TotalPages : 1;
        pageText.text = $"페이지 {inventory.CurrentPage + 1}/{totalPages}";
    }

    IEnumerator ChangePageWithDelay(Inventory inventory, Transform slotContainer, Text pageText)
    {
        isChangingPage = true;
        inventory.NextPage();
        RefreshInventoryUI(inventory, slotContainer, pageText);
        yield return new WaitForSeconds(pageChangeSpeed);
        isChangingPage = false;
    }

    void ShowTooltip(string itemName, Vector2 mousePos)
    {
        tooltipPanel.SetActive(true);
        tooltipText.text = itemName;
        RectTransform rect = tooltipPanel.GetComponent<RectTransform>();
        rect.position = mousePos + Vector2.right * 10;
    }

    void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }

    void TestAddItems()
    {
        Sprite tempIcon = Resources.Load<Sprite>("Sprites/TestIcon");
        
        Item herb = new Item("herb", "민트풀", 50, 99, tempIcon, 0);
        materialInventory.AddItem(herb);

        Item potion = new Item("potion", "빨간 물약", 5, 20, tempIcon, 0);
        potionInventory.AddItem(potion);

        RefreshUI();
    }
}