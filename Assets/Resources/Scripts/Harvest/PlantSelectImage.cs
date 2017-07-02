using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelectImage : SelectionnableUIElement
{
    public Plant plant;
    
    public new void onSelect()
    {
        HarvestPopUp.selectedPlant = this;
        base.onSelect();
    }

    public new void onUnselect()
    {
        base.onUnselect();
    }
}
