using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestPopUp : ClosableUI {

    public static HarvestField harvestPlant = null;

    static public PlantSelectImage selectedPlant = null;

    public override void onQuit()
    {
        base.onQuit();
        if (harvestPlant)
        {
            harvestPlant.onOk(selectedPlant.plant);
        }
    }

    public void onCancel()
    {
        base.onQuit();
        if (selectedPlant) selectedPlant.onUnselect();
        selectedPlant = null;
    }

    public void onSelectPlant(PlantSelectImage caller)
    {
        if(selectedPlant)
        {
            selectedPlant.onUnselect();
        }
        selectedPlant = caller;
    }
}