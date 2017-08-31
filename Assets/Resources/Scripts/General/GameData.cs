using UnityEngine;
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
    public static Population population = null;
    public static Stone stone = null;
    public static int actualMapFieldId = -1;
    private static Dictionary<int, State> mapStates = new Dictionary<int, State>();
    public static FightState playerFightState = null;
    
    //TUNING
    public float startHour = 12;
    public float realTimeAcceleration = 10;
    public float startMoney = 0;
    public int startMaxPopulation = 5;
    public int startStone = 0;

    public List<Item> startItems;
    
    private Text moneyText;
    private Text populationText;
    private Text stoneText;

    private Transform fieldHolder;

    private void Start()
    {
        //Load UI texts
        moneyText = GameObject.Find("TextMoneyValue").GetComponent<Text>();
        stoneText = GameObject.Find("TextStoneValue").GetComponent<Text>();
        // Getting the top level component there because population has 2 parts.
        populationText = GameObject.Find("TextPopulation").GetComponent<Text>();

        if (!activated)
        {
            activated = true;
            float realStartHour = DateTime.Now.Hour + (DateTime.Now.Minute / 60);
            timeHandler = new TimeHandler(startHour == 0 ? startHour : realStartHour,
                realTimeAcceleration);
            inventory = new Inventory(startItems);
            money = new Money(startMoney);
            population = new Population(startMaxPopulation);
            stone = new Stone(startStone);

            // Temp map stub
            mapStates.Add(0, new HarvestState());
            mapStates.Add(1, new MineState());
            mapStates.Add(2, new CropState());
            mapStates.Add(3, new HouseState());
            mapStates.Add(4, new LaboratoryState());
            mapStates.Add(5, new TownHallState());
            mapStates.Add(6, new StorageState());
            mapStates.Add(7, new BarrackState());

            onChangeState(0, mapStates[0].type);
            onChangeState(1, mapStates[1].type);
            onChangeState(2, mapStates[2].type);
            onChangeState(3, mapStates[3].type);
            onChangeState(4, mapStates[4].type);
            onChangeState(5, mapStates[5].type);
            onChangeState(6, mapStates[6].type);
            onChangeState(7, mapStates[7].type);

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
        if(populationText)
        {
            populationText.transform.GetChild(0).GetComponent<Text>().text = population.getPopulation().ToString() + "/";
            populationText.transform.GetChild(1).GetComponent<Text>().text = population.getMaxPopulation().ToString();
        }
        if(stoneText)
            stoneText.text = stone.getAmount().ToString();
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
            case State.Type.Barrack:
                m = Resources.Load<Material>("Materials/Map/BarrackMaterial");
                break;
            case State.Type.Crop:
                m = Resources.Load<Material>("Materials/Map/CropMaterial");
                break;
            case State.Type.House:
                m = Resources.Load<Material>("Materials/Map/HouseMaterial");
                break;
            case State.Type.Laboratory:
                m = Resources.Load<Material>("Materials/Map/LaboratoryMaterial");
                break;
            case State.Type.Storage:
                m = Resources.Load<Material>("Materials/Map/StorageMaterial");
                break;
            case State.Type.TownHall:
                m = Resources.Load<Material>("Materials/Map/TownHallMaterial");
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
