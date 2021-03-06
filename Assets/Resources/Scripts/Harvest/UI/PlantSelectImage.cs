﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelectImage : SelectionnableUIElement
{
    public Plant plant;
    
    public new void onSelect()
    {
        if (HarvestPopUp.selectedPlant)
            HarvestPopUp.selectedPlant.onUnselect();
        HarvestPopUp.selectedPlant = this;
        base.onSelect();
    }
}
