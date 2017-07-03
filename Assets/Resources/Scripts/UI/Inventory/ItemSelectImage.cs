using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectImage : SelectionnableUIElement {

    public Item item = null;

    public new void onSelect()
    {
        if (InventoryUI.selectedItem)
            InventoryUI.selectedItem.onUnselect();
        InventoryUI.selectedItem = this;
        base.onSelect();
    }

}
