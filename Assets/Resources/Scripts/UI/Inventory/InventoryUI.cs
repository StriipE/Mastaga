using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : ClosableUI {

    public static InventoryUI inventoryUIinstance;
    private ItemSelectImage selectedItem;
    
    private List<Item> actualItems = new List<Item>();
    public Image itemData = null;
    public ItemSelectImage itemBackgroundImage = null;
    public Image itemPanel = null;
    public Text itemCountText = null;

    private int selectedItemCount = 1;
        
    List<ItemSelectImage> itemElements = new List<ItemSelectImage>();

    Predicate<Item> itemCategory = x => x.count > 0;

    private float refreshTimer;
    public static float refreshTime = 1.5f;

    public int itemCountPerLine = 4;
    public int itemLineCount = 4;

    private void Update()
    {
        if(this.gameObject.activeSelf)
        {
            refreshTimer += Time.deltaTime;
            if(refreshTimer > refreshTime)
            {
                refresh();
                refreshTimer = 0;
            }
        }
    }

    public new void onActivate()
    {
        inventoryUIinstance = this;
        if (!this.gameObject.activeSelf && !GameData.popUpActive)
        {
            base.onActivate();
            refresh();
        }
    }

    public bool refresh()
    {
        if (PlayerData.inventory.items.FindAll(itemCategory).Count == 0)
        {
            cleanItemList();
            itemData.gameObject.SetActive(false);
            return false;
        }
        else
        {
            itemData.gameObject.SetActive(true);
            generateItemList(itemCategory);
            return true;
        }
    }

    public void onSelectValue()
    {
        if (selectedItem)
        {
            this.selectedItemCount = (int)itemData.transform.GetChild(3)
                .gameObject.GetComponent<Slider>().value;
            itemData.transform.GetChild(2).gameObject.GetComponent<Text>().text =
                selectedItemCount.ToString() +
                " / " + selectedItem.item.count.ToString();
            itemData.transform.GetChild(5).gameObject.GetComponent<Text>().text =
                (selectedItem.item.sellPrice * selectedItemCount).ToString();
        }
    }

    public new void onQuit()
    {
        base.onQuit();
        this.itemPanel.gameObject.transform.DetachChildren();
        foreach(SelectionnableUIElement el in this.itemElements)
        {
            Destroy(el.gameObject);
        }
        this.itemElements.Clear();
        cleanItemList();
    }

    public void selectElement(ItemSelectImage item)
    {
        this.selectedItem = item;
        printItemData(item.item);
    }

    public ItemSelectImage getSelectedItem()
    {
        return this.selectedItem;
    }

    private void printItemData(Item item)
    {
        itemData.transform.GetChild(0).gameObject.GetComponent<Image>().color = item.image.color;
        itemData.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.name;
        itemData.transform.GetChild(2).gameObject.GetComponent<Text>().text = 
            "1 / " + item.count.ToString();
        itemData.transform.GetChild(3).gameObject.GetComponent<Slider>().maxValue = item.count;
        //itemData.transform.GetChild(4).gameObject.GetComponent<Image>().color = item.image.color;
        itemData.transform.GetChild(5).gameObject.GetComponent<Text>().text = 
            (item.sellPrice * selectedItemCount).ToString();
    }

    private void sellItem()
    {
        if(selectedItem)
        {
            this.selectedItem.item.count -= selectedItemCount;
            PlayerData.money += selectedItemCount * selectedItem.item.sellPrice;
            selectedItemCount = selectedItem.item.count > 0 ? 1 : 0;
            //Set count
            itemData.transform.GetChild(2).gameObject.GetComponent<Text>().text =
                selectedItemCount.ToString() + " / " + selectedItem.item.count.ToString();
            itemData.transform.GetChild(5).gameObject.GetComponent<Text>().text =
                selectedItem.item.sellPrice.ToString();
            itemData.transform.GetChild(3).gameObject.GetComponent<Slider>().value = selectedItemCount;
            itemData.transform.GetChild(3).gameObject.GetComponent<Slider>().maxValue = 
                selectedItem.item.count;
            //TODO SET MONEY
            Debug.Log("Money = " + PlayerData.money);
            if(selectedItem.item.count == 0)
            {
                refresh();
            }
        }
    }

    private void cleanItemList()
    {
        this.itemPanel.gameObject.transform.DetachChildren();
        foreach (SelectionnableUIElement el in this.itemElements)
        {
            Destroy(el.gameObject);
        }
        this.itemElements.Clear();
        actualItems.Clear();
        itemData.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        itemData.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
        itemData.transform.GetChild(2).gameObject.GetComponent<Text>().text = "";
        itemData.transform.GetChild(3).gameObject.GetComponent<Slider>().maxValue = 0;
        itemData.transform.GetChild(5).gameObject.GetComponent<Text>().text = "";
    }

    private void generateItemList(Predicate<Item> predicate)
    {
        cleanItemList();
        selectedItem = null;

        int count = 0;
        
        actualItems = PlayerData.inventory.items.FindAll(predicate);
        bool first = true;
        foreach (Item item in actualItems)
        {
            if (count >= itemCountPerLine * itemLineCount) break;
            int x = count % itemCountPerLine * 95;
            int y = count / itemCountPerLine * -95;
            ItemSelectImage el = Instantiate(itemBackgroundImage);

            el.item = item;
            el.gameObject.transform.SetParent(this.itemPanel.gameObject.transform, false);
            if (first)
            {
                first = false;
                this.selectedItem = el;
                el.onSelect();
            }

            Image itemImage = Instantiate(el.item.image);
            itemImage.gameObject.transform.SetParent(el.gameObject.transform, false);

            Text countText = Instantiate(this.itemCountText);
            countText.gameObject.transform.SetParent(itemImage.gameObject.transform, false);
            countText.text = item.count.ToString();

            this.itemElements.Add(el);
            el.gameObject.transform.Translate(x, y, 0);

            ++count;
        }
    }

}
