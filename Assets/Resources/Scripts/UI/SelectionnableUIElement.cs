using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionnableUIElement : MonoBehaviour {

    public Image selectedImage = null;
    public Image unselectedImage = null;

    Sprite selectedSave = null;
    Color color;
    Material material = null;
    bool raycastTarget = false;
    public virtual void onSelect()
    {
        if (!selectedSave)
        {
            selectedSave = unselectedImage.sprite;
            color = unselectedImage.color;
            material = unselectedImage.material;
            raycastTarget = unselectedImage.raycastTarget;

        }
        this.gameObject.GetComponent<Image>().sprite = selectedImage.sprite;
        this.gameObject.GetComponent<Image>().color = selectedImage.color;
        this.gameObject.GetComponent<Image>().material = selectedImage.material;
        this.gameObject.GetComponent<Image>().raycastTarget = selectedImage.raycastTarget;
    }

    public virtual void onUnselect()
    {
        this.gameObject.GetComponent<Image>().sprite = selectedSave;
        this.gameObject.GetComponent<Image>().color = color;
        this.gameObject.GetComponent<Image>().material = material;
        this.gameObject.GetComponent<Image>().raycastTarget = raycastTarget;
    }
}
