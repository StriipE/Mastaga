using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPlantUI : MonoBehaviour {

    public HarvestPopUp popup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onSelect()
    {
        popup.onSelectPlant();
    }
}
