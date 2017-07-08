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
    public float MaxHP;
    public float MaxMP;
    public float ManaRegeneration;

    private float HP;
    private float MP;
    private float experience;
    private float levelUpExperience;
    private int level;

    private float ONE_SECOND_TIMER = 1f;
    private float ONE_TENTH_SECOND_TIMER = 0.1f;

    private Attack[] playerAttacks { get { return gameObject.GetComponents<Attack>(); } }

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
	
    void Awake()
    {

    }
    // Use this for initialization
	void Start ()
    {
        setPlayerAttacks();
        HP = MaxHP;
        MP = MaxMP;
        level = 1;
        experience = 0;
        levelUpExperience = 10;
        lifeBar.setValues(HP, MaxHP);
        manaBar.setValues(MP, MaxMP);
        experienceBar.setValues(experience, levelUpExperience);
        DisplayLevel();
    }


	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        updatePlayerRengens();
        lifeBar.renderValues();
        manaBar.renderValues();
        experienceBar.renderValues();
    }

    public void getDamaged(float damage)
    {
        HP -= damage;
        lifeBar.updateCurrent(HP);
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
        if (attackedEnemy != null && fireball.GetMPCost() <= MP)
        { 
            fireball.castAttackOnEnemy(attackedEnemy);
            MP -= fireball.GetMPCost();
            manaBar.updateCurrent(MP);
        }
    }

    public void onMagicEndEvent()
    {

    }

    public void onHealEvent()
    {
        // TODO Implement spell instead of having hardcoded logic here.
        if (MP > 30 && HP < MaxHP)
        {
            if (HP + 200 < MaxHP)
                HP += 200;
            else
                HP = MaxHP;

            MP -= 30;
        }

        lifeBar.updateCurrent(HP);
        manaBar.updateCurrent(MP);
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
        experience += _experience;
        if (experience >= levelUpExperience)
        {
            levelUp();
            experience -= levelUpExperience;
            levelUpExperience += 10;
        }
        experienceBar.setValues(experience, levelUpExperience);
    }

    private void setPlayerAttacks()
    {
        gameObject.AddComponent<PlayerBasicAttack>();
        gameObject.AddComponent<PlayerFireball>();
    }

    private void updatePlayerRengens()
    {
        ONE_TENTH_SECOND_TIMER -= Time.deltaTime;
        if (ONE_TENTH_SECOND_TIMER < 0)
        {
            if (MP + ManaRegeneration / 10 < MaxMP)
            {
                MP += ManaRegeneration / 10;
                ONE_TENTH_SECOND_TIMER = 0.1f;
            }
            else
            {
                MP = MaxMP;
                ONE_TENTH_SECOND_TIMER = 0.1f;
            }

            manaBar.updateCurrent(MP);
        }
    }

    private void levelUp()
    {
        level++;
        Strength += .5f;
        MagicPower += .5f;
        MaxHP += 50;
        lifeBar.updateUpper(MaxHP);
        MaxMP += 10;
        manaBar.updateUpper(MaxMP);
        ManaRegeneration += .5f;
        DisplayLevel();
    }

    private void DisplayLevel()
    {
        GameObject TextPlayer = GameObject.Find("TextPlayerName");
        TextPlayer.GetComponent<Text>().text = "PlayerName - Level " + level.ToString();
    }
}
