﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLight : MonoBehaviour {

    public float speed = 10;
    // Use this for initialization
    void Start()
    {
        
    }
    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime);
        transform.LookAt(Vector3.zero);


    }
}
