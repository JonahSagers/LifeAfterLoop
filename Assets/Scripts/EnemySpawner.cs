using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 spawnLoc;
    public GameObject enemyPre;
    public GameObject newEnemy;
    public int maxEnemies;
    // Start is called before the first frame update
    void Start()
    {
        StopCoroutine(SpawnWave());
        maxEnemies = 10;
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnWave()
    {
        int i = 0;
        while(i < maxEnemies){
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(1,5));
            i++;
        }
        
    }

    void SpawnEnemy()
    {
        spawnLoc = new Vector3(Random.Range(-15,17),Random.Range(-9,12),0);
        newEnemy = Instantiate(enemyPre, spawnLoc, Quaternion.identity);
    }
}
