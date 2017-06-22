using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestPlant : MonoBehaviour {

    public Renderer rend;

    public Material dirt;
    public Material growing;
    public Material alive;
    public Material old;

    public int growTime = 10;
    public int aliveTime = 120;

    enum HarvestState
    {
        Dirt,
        Growing,
        Alive,
        Old
    }

    private HarvestState state;
    private float timer = 0.0f;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if(state != HarvestState.Dirt)
        {
            timer += Time.deltaTime;

            switch (state)
            {
                case HarvestState.Growing:
                    if (timer > growTime)
                    {
                        this.state = HarvestState.Alive;
                        this.rend.material = alive;
                    }
                    break;
                case HarvestState.Alive:
                    if(timer > aliveTime * 0.75)
                    {
                        this.state = HarvestState.Old;
                        this.rend.material = old;
                    }
                    break;
                case HarvestState.Old:
                    if (timer > aliveTime)
                    {
                        this.state = HarvestState.Dirt;
                        this.rend.material = dirt;
                    }
                    break;
            }
        }
    }

    public void OnMouseDown()
    {
        if (this.state == HarvestState.Dirt)
        {
            GameObject.Find("UI").transform.Find("PopUpField").gameObject.SetActive(true);
            HarvestPopUp.harvestPlant = this;
        }
    }


    public void onOk()
    {
        this.state = HarvestState.Growing;
        this.rend.material = this.growing;
        this.timer = 0.0f;
    }

    
}
