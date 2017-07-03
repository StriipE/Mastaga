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
        return dropRate[index] < Random.value;
    }

}
