using Assets.Resources.Scripts;
using Assets.Resources.Scripts.Attacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Renderer rend;
    public Material standardMaterial;
    public Material attackMaterial;
    public GenericProgressBar lifeBar;
    public float Strength;

    private float HP;
    private float MaxHP;
    private Attack[] playerAttacks { get { return gameObject.GetComponents<Attack>(); } }

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
	
    void Awake()
    {

    }
    // Use this for initialization
	void Start ()
    {
        setPlayerAttacks();
        Strength = 3;
        HP = 1500;
        MaxHP = HP;
        lifeBar.setValues(HP, MaxHP);
    }


	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        lifeBar.renderValues();
    }

    public void getDamaged(float damage)
    {
        HP -= damage;
        lifeBar.updateCurrent(HP);
    }

    public void onAttackEvent()
    {
        this.rend.material = attackMaterial;
        Enemy attackedEnemy = getFirstEnemy();
        if (attackedEnemy != null)
            gameObject.GetComponent<PlayerBasicAttack>().castAttackOnEnemy(attackedEnemy);       
    }

    public void onAttackEndEvent()
    {
        this.rend.material = standardMaterial;
    }

    public void onMagicEvent()
    {

    }

    public void onMagicEndEvent()
    {

    }

    public void onHealEvent()
    {

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
    private void setPlayerAttacks()
    {
        gameObject.AddComponent<PlayerBasicAttack>();
        gameObject.AddComponent<PlayerFireball>();
    }
}
