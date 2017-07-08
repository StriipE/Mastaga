using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler {
    
	private float realTimeAcceleration;

    private float actualTime = 0;

    public TimeHandler(float startHour, float realTimeAcceleration)
    {
        this.actualTime = startHour * 60;
        this.realTimeAcceleration = realTimeAcceleration;
    }
    
    public void Update ()
    {
        this.actualTime += Time.deltaTime * realTimeAcceleration;
	}

    public float getActualTime()
    {
        return actualTime;
    }
}
