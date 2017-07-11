using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestState : State {

    public List<FieldState> fields;

    public const int fieldCount = 15;

    public HarvestState() : base(Type.Harvest)
    {
        this.fields = new List<FieldState>();
        for (int i = 0; i < fieldCount; i++)
        {
            this.fields.Add(new FieldState());
        }
    }

    // Update is called once per frame
    public override void Update ()
    {
		foreach(FieldState field in fields)
        {
            field.Update();
        }
	}
    
}
