using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 spawnLoc;
    public GameObject enemyPre;
    public GameObject newEnemy;
    public int maxEnemies;
    public SigilHandler sigil;
    public int difficulty;
    public TextDisplay text;
    public StaticHandler handler;
    public Image screenFlash;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        if(handler.tutorial){
            sigil.ticking = false;
            StartCoroutine(text.IntroSequence());
        } else {
            screenFlash.enabled = false;
            sigil.ticking = true;
            Destroy(text.tutorialChain);
            Destroy(text.tutorialEnemy.gameObject);
            AstarPath.active.Scan();
            StartCoroutine(NextWave());
        }
        handler.tutorial = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(sigil.immortality == false && sigil.enemyCount == 0 && sigil.ticking == true){
            StartCoroutine(NextWave());
        }
    }
    IEnumerator SpawnWave()
    {
        StartCoroutine(text.ShowText("Wave " + (difficulty / 5).ToString(), 0.5f));
        int i = 0;
        while(i < maxEnemies && sigil.immortality == true){
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(1,3));
            i++;
        }
        
    }

    public IEnumerator NextWave()
    {
        StopCoroutine(SpawnWave());
        difficulty += 5;
        maxEnemies = 5 + difficulty;
        sigil.CreateSigil(Mathf.Clamp(difficulty / 5 + 4,0,7), difficulty / 5 + 2);
        StartCoroutine(SpawnWave());
        yield return new WaitForSeconds(0);
    }

    public void SpawnEnemy()
    {
        spawnLoc = new Vector3(Random.Range(-15,17),Random.Range(-9,12),0);
        newEnemy = Instantiate(enemyPre, spawnLoc, Quaternion.identity);
    }
}
