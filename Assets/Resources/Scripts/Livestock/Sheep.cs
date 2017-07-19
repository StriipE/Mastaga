using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour {

    public float thinkDelay = 3;
    public float speed = 1f;

    private float thinkTimer = 0;

    private Vector3 direction = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        thinkTimer += Time.deltaTime;
        if(thinkTimer >= thinkDelay)
        {
            thinkTimer = 0;
            direction = new Vector3(Random.Range(-1,1), 0, Random.Range(-1,1));
        }
        //transform.rotation = Quaternion.LookRotation(direction) ;
        this.transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.direction = new Vector3(direction.x > 0 ? -direction.x : direction.x, 0, direction.z > 0 ? -direction.z : direction.z);
    }
}
