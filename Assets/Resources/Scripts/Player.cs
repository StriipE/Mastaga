using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    

    public Renderer rend;
    public Material standardMaterial;
    public Material attackMaterial;

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
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
