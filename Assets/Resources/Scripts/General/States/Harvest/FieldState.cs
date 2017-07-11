using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldState : State{

    public enum HarvestStatus
    {
        Dirt,
        Growing,
        Alive,
        Old
    }

    public HarvestStatus harvestState;

    public float timer = 0.0f;
    public float dropTimer = 0.0f;

    public string plantPrefabPath;

    public FieldState() : base(Type.Harvest)
    {
    }
    
    public override void Update() 
    {
        if(this.harvestState != HarvestStatus.Dirt)
        {
            this.timer += Time.deltaTime;
        }
    }

}
