using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ENEMY SPAWNER SCRIPT */

public class EnemySpawner : MonoBehaviour {

    public GameObject[] spawners;           // EnemyAI spawn points
    public GameObject enemyAI;              // Enemy object
    public int desiredEnemies = 15;         // Desired number of enemies to exist on field
    public int currentEnemies = 0;          // Count of current existing enemies on field


    void Start() { 

        // POPULATE SPAWNERS LIST WITH ALL OBJECT SPAWN POINTS
        spawners = new GameObject[GameObject.FindGameObjectsWithTag("Spawner").Length];


        for ( int i = 0; i < spawners.Length; i++) { 

            spawners[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update() {

        // COUNT THE NO OF ENEMIES ON FIELD
        currentEnemies = GameObject.FindGameObjectsWithTag("EnemyAI").Length;

        // IF THERE IS LESS NO OF DESIRED ENEMIES ON FIELD ... SPAWN MISSING DESIRED ENEMIES
        if ( currentEnemies < desiredEnemies ) {

            Debug.Log("New enemy spawned : Spider");
            SpawnEnemy();
        }
                
    }

    public void SpawnEnemy() {

        // GENERATE A RADOM NO BETWEEN 0 AND THE NO OF SPAWNERS
        int randomSpawnIndex = Random.Range(0, spawners.Length);
        
        // SPAWN ENEMY AT A RANDOM SPAWNER
        Instantiate(    enemyAI,
                        spawners[randomSpawnIndex].transform.position, 
                        spawners[randomSpawnIndex].transform.rotation);
    }
}
