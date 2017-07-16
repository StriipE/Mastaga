using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState {

    public float Strength;
    public float MagicPower;
    public float ManaRegeneration;

    public float HP;
    public float MP;
    public float MaxHP;
    public float MaxMP;
    public float experience;
    public float levelUpExperience;
    public int level;

    private float ONE_SECOND_TIMER = 1f;
    private float ONE_TENTH_SECOND_TIMER = 0.1f;

    public void update()
    {
        ONE_SECOND_TIMER -= Time.deltaTime;
        ONE_TENTH_SECOND_TIMER -= Time.deltaTime;
        if (ONE_TENTH_SECOND_TIMER < 0)
        {
            GameData.playerFightState.MP += GameData.playerFightState.ManaRegeneration / 10;
            if (GameData.playerFightState.MP > GameData.playerFightState.MaxMP)
            {
                GameData.playerFightState.MP = GameData.playerFightState.MaxMP;
            }
            ONE_TENTH_SECOND_TIMER = 0.1f;
        }
        if(ONE_SECOND_TIMER < 0)
        {
            GameData.playerFightState.HP += 0.5f;
            if (GameData.playerFightState.HP > GameData.playerFightState.MaxHP)
            {
                GameData.playerFightState.HP = GameData.playerFightState.MaxHP;
            }
            ONE_SECOND_TIMER = 1;
        }
    }

}
