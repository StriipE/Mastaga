using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public ICountObserver observer = null;

    public string name = null;
    public Image image = null;

    public int sellPrice = 0;
    public int buyPrice = 0;
    public int count = 0;

    //Harvest
    
    //If seed equals true, the item hold is the growing plant.
    //We do that because there is no purpose in having a seed in 3d part.
    //If we want to change that, we have to create a child class for implementing shits.
    public bool seed = false;

    //Craft
    //Not yet

    //Fight Mode
    public bool weapon = false;

    protected void onAdd(float count)
    {
        if(observer != null)
        {
            observer.notify(count);
        }
    }

}
