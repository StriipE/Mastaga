using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionnableUIElement : MonoBehaviour {

    public Image selectedImage = null;
    public Image unselectedImage = null;

    public virtual void onSelect()
    {
        this.gameObject.GetComponent<Image>().sprite = selectedImage.sprite;
        this.gameObject.GetComponent<Image>().color = selectedImage.color;
        this.gameObject.GetComponent<Image>().material = selectedImage.material;
        this.gameObject.GetComponent<Image>().raycastTarget = selectedImage.raycastTarget;
    }

    public virtual void onUnselect()
    {
        this.gameObject.GetComponent<Image>().sprite = unselectedImage.sprite;
        this.gameObject.GetComponent<Image>().color = unselectedImage.color;
        this.gameObject.GetComponent<Image>().material = unselectedImage.material;
        this.gameObject.GetComponent<Image>().raycastTarget = unselectedImage.raycastTarget;
    }
}
