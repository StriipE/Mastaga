using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningCube : MonoBehaviour {

    public int countToDestruction;
    List<ParticuleText> particulesText = new List<ParticuleText>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ParticuleText removedItem = null;
        foreach (ParticuleText item in particulesText)
        {
            item.Update();
            if(item.isOver())
            {
                removedItem = item;
            }
        }
        if(removedItem != null)
        {
            particulesText.Remove(removedItem);
        }
    }

    private void OnMouseDown()
    {
        countToDestruction -= 1;
        
        if (countToDestruction == 0)
        {
            cleanUp();
            Destroy(this.gameObject);
            return;
        }
        particulesText.Add(new ParticuleText(this.gameObject, "HIT", 1));
    }

    private void cleanUp()
    {
        foreach (ParticuleText item in particulesText)
        {
            item.destroy();
        }
        particulesText.Clear();
    }
}
