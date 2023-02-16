using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMove : MonoBehaviour
{
    public float health;
    public SpriteRenderer render;
    public bool chained;
    public Animator anim;
    public GameObject enemyPre;
    public GameObject newEnemy;
    public List<GameObject> attachedChains;
    public SigilHandler sigil;
    public EnemySpawner spawner;
    
    // Start is called before the first frame update
    void Awake()
    {
        sigil = GameObject.Find("Sigil").GetComponent<SigilHandler>();
        spawner = GameObject.Find("Enemy Handler").GetComponent<EnemySpawner>();
        sigil.enemyCount += 1;
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;
        health = 10 + 2 * spawner.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Chained", chained);
        if(chained){
            gameObject.layer = 10;
            GetComponent<AIPath>().canMove = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }
    void FixedUpdate()
    {
        if(health < 5 * spawner.difficulty){
            health += 0.02f;
        }
        if(health <= 0){
            if(sigil.immortality == true){
                int i = 0;
                while(i <= 2){
                    newEnemy = Instantiate(GameObject.Find("Enemy Handler").GetComponent<EnemySpawner>().enemyPre, transform.position, Quaternion.identity);
                    newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-10,10),Random.Range(-10,10),0);
                    i += 1;
                }
            }
            for(var j = attachedChains.Count - 1; j > -1; j--){
                Destroy(attachedChains[j]);
            }
            AstarPath.active.Scan();
            sigil.enemyCount -= 1;
            Destroy(gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D hit)
    {
        if(hit.gameObject.layer == 7){
            hit.gameObject.GetComponent<Move>().StartCoroutine(hit.gameObject.GetComponent<Move>().TakeDamage(10));
            hit.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize((transform.position - hit.gameObject.transform.position)) * -500);
            GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize((transform.position - hit.gameObject.transform.position)) * 200);
        }
    }
    


    public IEnumerator TakeDamage(float damage)
    {
        if(!chained){
            GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize((transform.position - GameObject.Find("Player").transform.position)) * 500);
        }
        health -= damage;
        render.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        render.color = Color.white;
    }
}
