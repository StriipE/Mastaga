using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : ClosableUI {

    public static ItemSelectImage selectedItem;

    private List<Item> actualItems = new List<Item>();
    public Image categoryImage = null;
    public ItemSelectImage itemBackgroundImage = null;
    public Image itemPanel = null;
    public Text itemCountText = null;

    List<SelectionnableUIElement> itemElements = new List<SelectionnableUIElement>();

    public new void onActivate()
    {
        if (!this.gameObject.activeSelf && !GameData.popUpActive)
        {
            base.onActivate();
            generateItemList(x => x.count > 0);
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
    }

    public int itemCountPerLine  = 4;
    public int itemLineCount = 4;
    public void generateItemList(Predicate<Item> predicate)
    {
        actualItems.Clear(); 

        int count = 0;
        
        actualItems = PlayerData.inventory.items.FindAll(predicate);
        foreach (Item item in actualItems)
        {
            Debug.Log(this.itemPanel.gameObject.transform.childCount + " " + item.name);
            if (count >= itemCountPerLine * itemLineCount) break;
            int x = count % itemCountPerLine * 95;
            int y = count / itemCountPerLine * -95;
            ItemSelectImage el = Instantiate(itemBackgroundImage);
            el.item = item;
            el.gameObject.transform.SetParent(this.itemPanel.gameObject.transform, false);

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
