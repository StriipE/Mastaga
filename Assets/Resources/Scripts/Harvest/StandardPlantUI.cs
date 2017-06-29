using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandardPlantUI : MonoBehaviour {

    public HarvestPopUp popup;
    public Plant plant;
    
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onSelect()
    { 
        popup.onSelectPlant(this);
        GetComponent<Image>().color = Color.red;
    }

    public void onDeselect()
    {
        GetComponent<Image>().color = Color.white;
    }
}
