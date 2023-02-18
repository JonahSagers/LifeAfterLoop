using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static bool texting;
    public Image screenFlash;
    public SigilHandler sigil;
    public EnemyMove tutorialEnemy;
    public GameObject tutorialChain;
    public PlayerAttack weapon;
    public bool tutorialTrigger;
    public EnemySpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        texting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowText(string message, float speed)
    {
        yield return new WaitUntil(() => !texting);
        //this code dies if you try to queue up multiple messages
        //in a long term project, I would fix this
        //however, your mother
        texting = true;
        int i = 0;
        while(i <= message.Length){
            text.text = message.Substring(0,i);
            i += 1;
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(1f / speed);
        while(i > 0){
            i -= 1;
            text.text = message.Substring(0,i);
            yield return new WaitForSeconds(0.01f);
        }
        texting = false;
    }

    public IEnumerator IntroSequence()
    {
        weapon.canFlame = true;
        weapon.canChain = false;
        screenFlash.enabled = true;
        sigil.immortality = true;
        yield return new WaitForSeconds(1);
        StartCoroutine(ShowText("Death is not their end", 0.75f));
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowText("Break their loop", 0.5f));
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowText("Bring back death", 1f));
        yield return new WaitForSeconds(2.5f);
        screenFlash.enabled = false;
        StartCoroutine(ShowText("Right click to attack", 1f));
        yield return new WaitUntil(() => tutorialEnemy.health <= 0);
        weapon.canFlame = false;
        weapon.flamethrower = false;
        weapon.ps.Stop();
        StartCoroutine(ShowText("They always come back", 0.75f));
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowText("Don't kill them unless you need to", 0.75f));
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowText("Try locking them down instead", 0.75f));
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowText("Left click to chain", 0.75f));
        weapon.canChain = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(ShowText("Dropping the barrier", 0.75f));
        yield return new WaitForSeconds(2);
        Destroy(tutorialChain);
        AstarPath.active.Scan();
        yield return new WaitUntil(() => tutorialTrigger);
        StartCoroutine(ShowText("Congrats", 1f));
        yield return new WaitForSeconds(2);
        foreach(GameObject chain in GameObject.FindGameObjectsWithTag("Chain")){
            Destroy(chain);
        }
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            Destroy(enemy);
        }
        StartCoroutine(ShowText("Finally, breaking the loop", 0.75f));
        yield return new WaitForSeconds(2);
        spawner.SpawnEnemy();
        StartCoroutine(ShowText("Chain the enemy on top of the rune", 1f));
        sigil.CreateSigil(2, 1);
        sigil.ticking = true;
        weapon.canFlame = true;
        sigil.enemyCount += 1;
        AstarPath.active.Scan();
    }
}
