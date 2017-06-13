using Assets.Resources.Scripts;
using Assets.Resources.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour {

    private float timeBetweenSpawns = 3.0f;

	void Start () {
        spawnRandomEnemy();
    }
	
	void Update () {

        timeBetweenSpawns -= Time.deltaTime;
        if (timeBetweenSpawns < 0)
        {
            spawnRandomEnemy();
            timeBetweenSpawns = 3.0f;
        }

	}

    public void spawnEnemy<T>() where T : Enemy
    {
        GameObject enemyGO = new GameObject();
        enemyGO.AddComponent<T>();
    }

    private void spawnRandomEnemy()
    {
        int random = UnityEngine.Random.Range(1, 4);
        switch (random)
        {
            case 1:
                spawnEnemy<Spanker>();
                break;
            case 2:
                spawnEnemy<Banga>();
                break;
            case 3:
                spawnEnemy<Droums>();
                break;
            default:
                throw new ArgumentOutOfRangeException("EnemySpawner.spawnRandomEnemy Error : Unexpected random value.");
        }
    }
}
