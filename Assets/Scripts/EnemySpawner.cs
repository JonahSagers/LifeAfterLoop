using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 spawnLoc;
    public GameObject enemyPre;
    public GameObject newEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        //spawnLoc = new Vector3(Random.Range(-15,17),Random.Range(-9,12),0);
        //newEnemy = Instantiate(enemyPre, spawnLoc);
    }
}
