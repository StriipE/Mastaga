using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestPopUp : MonoBehaviour {

    public static HarvestPlant harvestPlant = null;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onOk()
    {
        this.gameObject.SetActive(false);
        if (harvestPlant)
        {
            harvestPlant.onOk();
        }
    }

    public void onCancel()
    {
        this.gameObject.SetActive(false);
    }

    public void onSelectPlant()
    {

    }
}