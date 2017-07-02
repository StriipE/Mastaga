using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantChild : MonoBehaviour {

    private Plant master = null;

    public void setMaster(Plant master)
    {
        this.master = master;
        for (int i = 0; i < this.gameObject.transform.childCount; ++i )
        {
            this.gameObject.transform.GetChild(i).gameObject.AddComponent<PlantChild>();
            this.gameObject.transform.GetChild(i).gameObject.GetComponent<PlantChild>().setMaster(master);
        }
    }

    public void OnMouseDown()
    {
        if (this.master && !GameData.popUpActive)
        {
            this.master.OnMouseDown();
        }
    }
}
