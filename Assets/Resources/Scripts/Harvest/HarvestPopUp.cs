using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestPopUp : MonoBehaviour {

    public static HarvestField harvestPlant = null;
    public static HarvestPopUp popup = null;
    private StandardPlantUI selectedPlantUI;

    // Use this for initialization
    void Start () {
        popup = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onOk()
    {
        this.gameObject.SetActive(false);
        if (harvestPlant)
        {
            harvestPlant.onOk(this.selectedPlantUI.plant);
        }
    }

    public void onCancel()
    {
        this.gameObject.SetActive(false);
    }

    public void onSelectPlant(StandardPlantUI caller)
    {
        if(this.selectedPlantUI)
        {
            this.selectedPlantUI.onDeselect();
        }
        this.selectedPlantUI = caller;
    }
}