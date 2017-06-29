using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static Inventory inventory = null;

    //Public non static var are only for the unity setup. 
    //Later we'll search for a non build option outside of the editor

    public float money = 0;

    public Inventory inventoryStart = null;

    public void Start()
    {
        if(!inventory)
        {
            inventory = this.inventoryStart;
            foreach(Item item in inventory.items)
            {
                item.count = 0;
            }
        }
    }


}
