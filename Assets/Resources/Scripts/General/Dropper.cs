using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour {

    //Syncronisation of list is done by index

    public List<string> dropItems;
    public List<int> dropCountMin;
    public List<int> dropCountMax;
    public List<float> dropRate;

    public bool willDrop(int index)
    {
        return dropRate[index] >= Random.value;
    }

    public void renderDrop(int index)
    {
        GameObject dropedItem = Instantiate(Resources.Load("Prefabs/Items/" + dropItems[index], typeof(GameObject))) as GameObject;

        dropedItem.transform.position = gameObject.transform.position - new Vector3(0, 0, gameObject.transform.localScale.z / 2);
    }
}
