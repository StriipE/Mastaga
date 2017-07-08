using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    
    public List<Item> items;
    
    public Inventory(List<Item> startItems)
    {
        items = new List<Item>(startItems);
    }

    public void addItem(String item, int count)
    {
        Item itemBuffer = this.items.Find(x => x.name == item);

        if (itemBuffer == null)
        {
            Item i = Resources.Load<Item>("Prefabs/Items/"+ item) as Item;
            i.count = count;
            items.Add(i);
        }
        else
        {
            itemBuffer.count += count;
        }
    }

    public Item findItemByName(string name)
    {
        return this.items.Find(x => x.gameObject.name == name);
    }

    public void removeItem(string name, int count)
    {
        Item item = this.items.Find(x => x.gameObject.name == name);
        item.count -= count;

        if (item.count == 0)
        { 
            Debug.Log("Item add : " + item.name);
        }
        else if (item.count > 0)
        {
            throw new Exception("Apprend à compter : " + item.name);
        }
    }

}
