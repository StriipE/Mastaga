using Assets.Resources.Scripts;
using Assets.Resources.Scripts.Attacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Renderer rend;
    public Material standardMaterial;
    public Material attackMaterial;
    public GenericProgressBar lifeBar;
    public GenericProgressBar manaBar;
    public GenericProgressBar experienceBar;

    public float Strength;
    public float MagicPower;
    public float ManaRegeneration;

    public float MaxHP;
    public float MaxMP;
    
    private Attack[] playerAttacks { get { return gameObject.GetComponents<Attack>(); } }

    // Use this for initialization
	void Start ()
    {
        if(GameData.playerFightState == null)
        {
            GameData.playerFightState = new FightState();
            GameData.playerFightState.HP = MaxHP;
            GameData.playerFightState.MP = MaxMP;
            GameData.playerFightState.Strength = Strength;
            GameData.playerFightState.MagicPower = MagicPower;
            GameData.playerFightState.ManaRegeneration = ManaRegeneration;
            GameData.playerFightState.MaxHP = MaxHP;
            GameData.playerFightState.MaxMP = MaxMP;
            GameData.playerFightState.level = 1;
            GameData.playerFightState.experience = 0;
            GameData.playerFightState.levelUpExperience = 10;
        }
        setPlayerAttacks();
        experienceBar.setValues(GameData.playerFightState.experience, GameData.playerFightState.levelUpExperience);
        DisplayLevel();
        lifeBar.setValues(GameData.playerFightState.HP, GameData.playerFightState.MaxHP);
        manaBar.setValues(GameData.playerFightState.MP, GameData.playerFightState.MaxMP);
    }
    
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        lifeBar.updateCurrent(GameData.playerFightState.HP);
        lifeBar.renderValues();
        manaBar.updateCurrent(GameData.playerFightState.MP);
        manaBar.renderValues();
        experienceBar.renderValues();
    }

    public void getDamaged(float damage)
    {
        GameData.playerFightState.HP -= damage;
        lifeBar.updateCurrent(GameData.playerFightState.HP);
        gameObject.AddComponent<DamageText>().renderDamage(gameObject, damage);
    }

    public void onAttackEvent()
    {
        this.rend.material = attackMaterial;
        Enemy attackedEnemy = getFirstEnemy();
        if (attackedEnemy != null && (attackedEnemy.transform.position.x - gameObject.transform.position.x) < 20 )
            gameObject.GetComponent<PlayerBasicAttack>().castAttackOnEnemy(attackedEnemy);       
    }

    public void onAttackEndEvent()
    {
        this.rend.material = standardMaterial;
    }

    public void onMagicEvent()
    {
        PlayerFireball fireball = gameObject.GetComponent<PlayerFireball>();
        Enemy attackedEnemy = getFirstEnemy();
        if (attackedEnemy != null && fireball.GetMPCost() <= GameData.playerFightState.MP)
        { 
            fireball.castAttackOnEnemy(attackedEnemy);
            GameData.playerFightState.MP -= fireball.GetMPCost();
            manaBar.updateCurrent(GameData.playerFightState.MP);
        }
    }

    public void onMagicEndEvent()
    {

    }

    public void onHealEvent()
    {
        // TODO Implement spell instead of having hardcoded logic here.
        if (GameData.playerFightState.MP > 30 && GameData.playerFightState.HP < GameData.playerFightState.MaxHP)
        {
            if (GameData.playerFightState.HP + 200 < GameData.playerFightState.MaxHP)
                GameData.playerFightState.HP += 200;
            else
                GameData.playerFightState.HP = GameData.playerFightState.MaxHP;

            GameData.playerFightState.MP -= 30;
        }

        lifeBar.updateCurrent(GameData.playerFightState.HP);
        manaBar.updateCurrent(GameData.playerFightState.MP);
    }

    public void onHealEndEvent()
    {

    }

    // Get enemy component of EnemyHandler
    private Enemy getFirstEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            return GameObject.FindGameObjectsWithTag("Enemy")[0].transform.GetComponent<Enemy>();
        else
            return null;
    }

    public void gainExperience(float _experience)
    {
        GameData.playerFightState.experience += _experience;
        if (GameData.playerFightState.experience >= GameData.playerFightState.levelUpExperience)
        {
            levelUp();
            GameData.playerFightState.experience -= GameData.playerFightState.levelUpExperience;
            GameData.playerFightState.levelUpExperience += 10;
        }
        experienceBar.setValues(GameData.playerFightState.experience, GameData.playerFightState.levelUpExperience);
    }

    private void setPlayerAttacks()
    {
        gameObject.AddComponent<PlayerBasicAttack>();
        gameObject.AddComponent<PlayerFireball>();
    }

    private void levelUp()
    {
        GameData.playerFightState.level++;
        GameData.playerFightState.Strength += .5f;
        GameData.playerFightState.MagicPower += .5f;
        GameData.playerFightState.MaxHP += 50;
        lifeBar.updateUpper(GameData.playerFightState.MaxHP);
        GameData.playerFightState.MaxMP += 10;
        manaBar.updateUpper(GameData.playerFightState.MaxMP);
        GameData.playerFightState.ManaRegeneration += .5f;
        DisplayLevel();
    }

    private void DisplayLevel()
    {
        GameObject TextPlayer = GameObject.Find("TextPlayerName");
        TextPlayer.GetComponent<Text>().text = "PlayerName - Level " + GameData.playerFightState.level.ToString();
    }
}
