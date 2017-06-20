using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Renderer rend;
    public Material standardMaterial;
    public Material attackMaterial;
    public GenericProgressBar lifeBar;

    private float HP;
    private float MaxHP;

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
	
    void Awake()
    {

    }
    // Use this for initialization
	void Start ()
    {
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
        Debug.Log("Click");
    }

    public void onAttackEndEvent()
    {
        this.rend.material = standardMaterial;
        Debug.Log("End Click");
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

}
