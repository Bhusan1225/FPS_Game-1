using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    //prefab
    public GameObject Enemy; 
    public float enemySpeed;
   
    private GameObject spawnedEnemy; // Reference to the spawned enemy
    private float spawnDelay = 5f;
    private bool isEnemyThere;
    
    void Update()
    {
        spawn();
      
    }

    private void spawn()
    {
        if (!isEnemyThere)
        {
            spawnedEnemy = Instantiate(Enemy, transform.position, transform.rotation);
            isEnemyThere = true;
            Invoke("resetSpawn", spawnDelay);
        }
    }

    void resetSpawn()
    {
        if (spawnedEnemy != null)
        {
            isEnemyThere = false;
        }
        
    }

    
}
