using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestField : MonoBehaviour {

    public Renderer rend;

    public Material dirt;
    public Plant plant;
    private int growTime = 10;
    private int aliveTime = 120;
    private int dropTime = 1;

    public enum HarvestState
    {
        Dirt,
        Growing,
        Alive,
        Old
    }

    private HarvestState state;
    private float timer = 0.0f;
    private float dropTimer = 0.0f;
	
	void Start () {
		
	}

    
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
                        this.rend.material = plant.alive;
                        this.plant.onAlive();
                    }
                    break;
                case HarvestState.Alive:
                    if(timer > aliveTime * 0.75)
                    {
                        this.state = HarvestState.Old;
                        this.rend.material = plant.old;
                        this.plant.onOld();
                    }
                    break;
                case HarvestState.Old:
                    if (timer > aliveTime)
                    {
                        this.state = HarvestState.Dirt;
                        this.rend.material = dirt;
                        this.plant.onDeath();
                        this.plant.gameObject.transform.DetachChildren();
                    }
                    break;
            }

            if(this.state != HarvestState.Growing)
            {
                this.dropTimer += Time.deltaTime;
                if(this.dropTimer >= this.dropTime)
                {
                    this.plant.onDroppable();
                }
            }
        }
    }

    public void OnMouseDown()
    { 
        if (this.state == HarvestState.Dirt && !GameData.popUpActive)
        {
            GameObject.Find("UI").transform.Find("PopUpField").GetComponent<HarvestPopUp>().onActivate();
            HarvestPopUp.harvestPlant = this;
        }
        else if(this.plant)
        {
            if (this.plant.isDroppable())
            {
                onDrop();
            }
        }
    }

    public void onDrop()
    {
        this.dropTimer = 0;
    }

    public void onOk(Plant plant)
    {    
        this.plant = Instantiate(plant.gameObject).GetComponent<Plant>();
        this.plant.gameObject.transform.SetParent(this.gameObject.transform);
        this.plant.setPosition(this.gameObject.transform.position.x, this.gameObject.transform.position.z);
        this.plant.GetComponent<Plant>().master = this;
        this.growTime = this.plant.growTime;
        this.aliveTime = this.plant.aliveTime;
        this.state = HarvestState.Growing;
        this.rend.material = this.plant.growing;
        this.timer = 0.0f;
    }
    
}
