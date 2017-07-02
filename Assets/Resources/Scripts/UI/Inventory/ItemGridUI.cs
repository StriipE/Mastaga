using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridUI : MonoBehaviour
{
    private List<Item> actualItems = new List<Item>();

    public void generateItemList()
    {
        actualItems.Clear();

        //Category system
        int count = 0;

        Debug.Log("Bonjour");
        actualItems = PlayerData.inventory.items.FindAll(x => x.count > 0);
        foreach (Item item in actualItems)
        {
            Debug.Log(count + " " + item.name);
            //this.itemPanel.gameObject.AddComponent(itemBackgroundImage.GetType());
            ++count;
        }
    }
}
