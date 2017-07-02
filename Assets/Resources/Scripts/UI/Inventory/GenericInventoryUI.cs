using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericInventoryUI : ClosableUI {

    public Image categoryImage = null;
    public Image itemBackgroundImage = null;
    public Image itemPanel = null;
    public ItemGridUI itemGrid = null;
    private SelectionnableUIElement selectedItem;

    public new void onActivate()
    {
        if (!this.gameObject.activeSelf)
        {
            base.onActivate();
            itemGrid.generateItemList();
        }
    }

    public void OnMouseDown()
    {
        
    }

    public new void onQuit()
    {
        base.onQuit();        
    }
}
