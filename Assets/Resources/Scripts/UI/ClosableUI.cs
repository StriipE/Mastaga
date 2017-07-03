using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosableUI : MonoBehaviour {

    public virtual void onActivate()
    {
        GameData.popUpActive = true;
        this.gameObject.SetActive(true);
    }

    public virtual void onQuit()
    {
        GameData.popUpActive = false;
        this.gameObject.SetActive(false);
    }

}
