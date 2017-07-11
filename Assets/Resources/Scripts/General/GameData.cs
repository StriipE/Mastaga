using UnityEngine;
using System;
using System.Collections.Generic;

public class GameData : MonoBehaviour {

    public static bool activated = false;
    public static bool popUpActive = false;
    public static TimeHandler timeHandler = null;
    
    public static Money money = null;
    public static Inventory inventory = null;
    public static int actualMapFieldId;
    private static Dictionary<int, State> mapStates = new Dictionary<int, State>();
    //TUNING
    public float startHour = 12;
    public float realTimeAcceleration = 10;
    public float startMoney = 0;
    public List<Item> startItems;

    private void Start()
    {
        if(!activated)
        {
            activated = true;
            float realStartHour = DateTime.Now.Hour + (DateTime.Now.Minute / 60);
            timeHandler = new TimeHandler(startHour == 0 ? startHour : realStartHour,
                realTimeAcceleration);
            inventory = new Inventory(startItems);
            money = new Money(startMoney);

            //STUB FOR HARVEST TEST
            for (int i = 0; i < 16; i++)
            {
                mapStates.Add(i, new HarvestState());
            }
        }        
    }

    public static bool mapDataExist()
    {
        return mapStates.ContainsKey(actualMapFieldId);
    }

    public static State getMapFieldData()
    {
        return mapStates[actualMapFieldId];
    }

    public static void setMapFieldData(State data)
    {
        mapStates[actualMapFieldId] = data;
    }

    private void Update()
    {
        timeHandler.Update();
        foreach (State state in mapStates.Values)
        {
            state.Update();
        }
    }
}
