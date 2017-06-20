using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Text lifeText;
    private RectTransform lifeBar;
    private float lifeBarSize;

    public Renderer rend;
    public Material standardMaterial;
    public Material attackMaterial;
    public float HP { get; set; }
    public float MaxHP { get; set; }

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
	
    void Awake()
    {
        lifeText = GameObject.Find("TextLife").transform.GetChild(1).GetComponent<Text>();
        lifeBar = GameObject.Find("CurrentHP").GetComponent<RectTransform>();
        lifeBarSize = lifeBar.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
    }
    // Use this for initialization
	void Start ()
    {
        MaxHP = 1500;
        HP = MaxHP;
        renderPlayerHP();
    }


	// Use this for initialization
	
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

    public void onLifeLoss()
    {
        renderPlayerHP();
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

    private void renderPlayerHP()
    {
        lifeText.text = HP.ToString() + "/" + MaxHP.ToString();
        lifeBar.offsetMax = new Vector2(- (lifeBarSize - (lifeBarSize * (HP / MaxHP))), 5);
    }
}
