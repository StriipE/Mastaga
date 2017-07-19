using System.Collections.Generic;
using UnityEngine;

public class HarvestField : MonoBehaviour {

    public Renderer rend;

    public Material dirt;
    public Plant plant;
    public int fieldId = -1;

    public FieldState state;

    private List<ParticuleText> dropParticules = new List<ParticuleText>();

    private static bool isSetup = false;

    private void Start()
    {
        if(!isSetup)
        {
            isSetup = true;
            GameData.setMapFieldData(new HarvestState());
        }
        this.state = ((HarvestState)GameData.getMapFieldData()).fields[fieldId];
        if(this.state.plantPrefabPath != null)
        {
            setPlantAfterStart();
        }
        updateState();
    }

    void Update()
    {
        ParticuleText dropParticulesRemoved = null;
        updateState();
        foreach (ParticuleText particule in dropParticules)
        {
            if(particule.isOver())
            {
                dropParticulesRemoved = particule;
            }
            particule.Update();
        }
        if (dropParticulesRemoved != null)
        {
            dropParticules.Remove(dropParticulesRemoved);
        }
    }

    private void updateState()
    {
        if (this.state.harvestState != FieldState.HarvestStatus.Dirt)
        {
            switch (this.state.harvestState)
            {
                case FieldState.HarvestStatus.Growing:
                    if (this.state.timer > this.plant.growTime)
                    {
                        this.state.harvestState = FieldState.HarvestStatus.Alive;
                        this.plant.onAlive();
                    }
                    break;
                case FieldState.HarvestStatus.Alive:
                    if (this.state.timer > this.plant.aliveTime * 0.75)
                    {
                        this.state.harvestState = FieldState.HarvestStatus.Old;
                        this.plant.onOld();
                    }
                    break;
                case FieldState.HarvestStatus.Old:
                    if (this.state.timer > this.plant.aliveTime)
                    {
                        this.state.harvestState = FieldState.HarvestStatus.Dirt;
                        this.state.plantPrefabPath = null;
                        this.plant.onDeath();
                        this.plant.gameObject.transform.DetachChildren();
                    }
                    break;
            }

            if (this.state.harvestState != FieldState.HarvestStatus.Growing)
            {
                this.state.dropTimer += Time.deltaTime;
                if (this.state.dropTimer >= this.plant.dropTime)
                {
                    this.plant.onDroppable();
                }
            }
        }
    }

    public void OnMouseDown()
    { 
        if (this.state.harvestState == FieldState.HarvestStatus.Dirt && !GameData.popUpActive)
        {
            GameObject.Find("UI").transform.Find("PopUpField").GetComponent<HarvestPopUp>().onActivate();
            HarvestPopUp.harvestPlant = this;
        }
        else if(this.plant)
        {
            if (this.plant.isDroppable())
            {
                this.plant.OnMouseDown();
            }
        }
    }

    public void onDrop(string dropText)
    {
        this.state.dropTimer = 0;
        dropParticules.Add(new ParticuleText(this.plant.gameObject, dropText, 1));
    }

    public void onOk(Plant plant)
    {

        this.state.plantPrefabPath = State.getPrefabPath(plant.gameObject);
        setupPlant();
        this.state.harvestState = FieldState.HarvestStatus.Growing;
        this.state.timer = 0.0f;
    }

    private void setPlantAfterStart()
    {
        setupPlant();
        if (this.state.timer > this.plant.aliveTime * 0.75)
        {
            this.state.harvestState = FieldState.HarvestStatus.Old;
            this.plant.onOld();
        }
        else if (this.state.timer > this.plant.aliveTime)
        {
            this.state.harvestState = FieldState.HarvestStatus.Dirt;
            this.state.plantPrefabPath = null;
            this.plant.onDeath();
            this.plant.gameObject.transform.DetachChildren();
        }
        else if (this.state.timer > this.plant.growTime)
        {
            this.state.harvestState = FieldState.HarvestStatus.Alive;
            this.plant.onAlive();
        }
        else
        {
            this.state.harvestState = FieldState.HarvestStatus.Growing;
            this.state.timer = 0.0f;
        }
    }

    public void setupPlant()
    {
        this.plant = Instantiate<Plant>(Resources.Load<Plant>(this.state.plantPrefabPath));
        this.plant.gameObject.transform.SetParent(this.gameObject.transform);
        this.plant.setPosition(this.gameObject.transform.position.x, this.gameObject.transform.position.z);
        this.plant.GetComponent<Plant>().master = this;
    }
    
}
