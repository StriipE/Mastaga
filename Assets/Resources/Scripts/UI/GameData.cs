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
    private static Dictionary<int, System.Object> mapDataIndex = new Dictionary<int, object>();
    //TUNING
    public float startHour = 12;
    public float realTimeAcceleration = 10;
    public float startMoney = 0;
    public List<Item> startItems;

    public enum FieldType
    {
        None, //=> Fight
        Harvest
    }

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
        }
    }

    public static bool mapDataExist()
    {
        return mapDataIndex.ContainsKey(actualMapFieldId);
    }

    public static System.Object getMapFieldData()
    {
        return mapDataIndex[actualMapFieldId];
    }

    public static void setMapFieldData(System.Object data)
    {
        mapDataIndex[actualMapFieldId] = data;
    }

    private void Update()
    {
        timeHandler.Update();
    }
}
