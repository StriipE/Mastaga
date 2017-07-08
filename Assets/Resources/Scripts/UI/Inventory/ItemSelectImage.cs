using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectImage : SelectionnableUIElement {

    public Item item = null;

    public new void onSelect()
    {
        if (InventoryUI.inventoryUIinstance.getSelectedItem())
            InventoryUI.inventoryUIinstance.getSelectedItem().onUnselect();
        InventoryUI.inventoryUIinstance.selectElement(this);
        base.onSelect();
    }
}
