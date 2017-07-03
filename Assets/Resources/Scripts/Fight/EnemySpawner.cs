using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemySpawnList;
    public float timeBetweenSpawns;

    private float setTimeBetweenSpawns;
	void Start () {
        spawnRandomEnemy();
        setTimeBetweenSpawns = timeBetweenSpawns;
    }
	
	void Update () {

        timeBetweenSpawns -= Time.deltaTime;
        if (timeBetweenSpawns < 0)
        {
            spawnRandomEnemy();
            timeBetweenSpawns = setTimeBetweenSpawns;
        }

	}

    private void spawnRandomEnemy()
    {
        int random = UnityEngine.Random.Range(1, enemySpawnList.Length);
        Instantiate(enemySpawnList[random - 1]);
    }

}
