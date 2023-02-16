using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 spawnLoc;
    public GameObject enemyPre;
    public GameObject newEnemy;
    public int maxEnemies;
    public SigilHandler sigil;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        StartCoroutine(NextWave());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(sigil.immortality == false && sigil.enemyCount == 0){
            StartCoroutine(NextWave());
        }
    }
    IEnumerator SpawnWave()
    {
        int i = 0;
        while(i < maxEnemies && sigil.immortality == true){
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(1,3));
            i++;
        }
        
    }

    IEnumerator NextWave()
    {
        StopCoroutine(SpawnWave());
        difficulty += 5;
        maxEnemies = 5 + difficulty;
        sigil.CreateSigil(Mathf.Clamp(difficulty / 5 + 2,0,7), difficulty / 5 + 2);
        StartCoroutine(SpawnWave());
        yield return new WaitForSeconds(0);
    }

    void SpawnEnemy()
    {
        spawnLoc = new Vector3(Random.Range(-15,17),Random.Range(-9,12),0);
        newEnemy = Instantiate(enemyPre, spawnLoc, Quaternion.identity);
    }
}
