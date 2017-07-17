﻿using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour {

    public static int fightId = 0;
    public static int harvestBuildId = 2;
    public static int mineBuildId = 3;

    public static bool activated = false;
    public static bool popUpActive = false;
    public static TimeHandler timeHandler = null;
    
    public static Money money = null;
    public static Inventory inventory = null;
    public static int actualMapFieldId = -1;
    private static Dictionary<int, State> mapStates = new Dictionary<int, State>();
    public static FightState playerFightState = null;
    
    //TUNING
    public float startHour = 12;
    public float realTimeAcceleration = 10;
    public float startMoney = 0;
    public List<Item> startItems;
    
    private Text moneyText;
    private Transform fieldHolder;

    private void Start()
    {
        //Load money text
        moneyText = GameObject.Find("TextMoneyValue").GetComponent<Text>();
        
        if (!activated)
        {
            activated = true;
            float realStartHour = DateTime.Now.Hour + (DateTime.Now.Minute / 60);
            timeHandler = new TimeHandler(startHour == 0 ? startHour : realStartHour,
                realTimeAcceleration);
            inventory = new Inventory(startItems);
            money = new Money(startMoney);

            //STUB FOR HARVEST TEST
            for (int i = 0; i < 15; i++)
            {
                if (i == 0)
                {
                    mapStates.Add(i, new HarvestState());
                }
                else
                {
                    mapStates.Add(i, new EmptyState());
                }
                onChangeState(i, mapStates[i].type);
            }
        }
        if (SceneManager.GetActiveScene().name == "Map")
        {
            fieldHolder = GameObject.Find("Fields").transform;
            foreach (int index in mapStates.Keys)
            {
                onChangeState(index, mapStates[index].type);
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
        if (playerFightState != null)
        {
            playerFightState.update();
        }
        timeHandler.Update();
        foreach (State state in mapStates.Values)
        {
            state.Update();
        }
        if(moneyText)
        {
            moneyText.text = money.toString();
        }
    }

    private void onChangeState(int id, State.Type state)
    {
        if(SceneManager.GetActiveScene().name != "Map" || fieldHolder == null || id == -1)
        {
            return;
        }

        Material m = null;

        switch (state)
        {
            case State.Type.Empty:
                m = Resources.Load<Material>("Materials/Map/EmptyMaterial");
                break;
            case State.Type.Cleared:
                m = Resources.Load<Material>("Materials/Map/EmptyMaterial");
                break;
            case State.Type.Harvest:
                m = Resources.Load<Material>("Materials/Map/HarvestMaterial");
                break;
            case State.Type.Mine:
                m = Resources.Load<Material>("Materials/Map/MineMaterial");
                break;
            default:
                return;
        }

        fieldHolder.GetChild(id).GetComponent<Renderer>().material = m;
        fieldHolder.GetChild(id).GetComponent<MapField>().actualMaptype = state;
    }

    public void clearActualState()
    {
        
        mapStates[actualMapFieldId].type = State.Type.Cleared;
        fieldHolder.GetChild(actualMapFieldId).GetComponent<MapField>().actualMaptype = State.Type.Cleared;
    }
}