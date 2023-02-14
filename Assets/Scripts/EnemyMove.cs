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
    
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;
        health = 10;
        //PLEASE FOR THE LOVE OF GOD MAKE A SEPARATE SCRIPT FOR EACH ENEMY TYPE
        //but that's for later
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
        if(health < 10){
            health += 0.02f;
        }
    }

    public void OnCollisionEnter2D(Collision2D hit)
    {
        if(hit.gameObject.layer == 7){
            hit.gameObject.GetComponent<Move>().StartCoroutine(hit.gameObject.GetComponent<Move>().TakeDamage(1));
        }
    }

    public IEnumerator TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0){
            int i = 0;
            while(i <= 2){
                newEnemy = Instantiate(GameObject.Find("Enemy Handler").GetComponent<EnemySpawner>().enemyPre, transform.position, Quaternion.identity);
                newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-10,10),Random.Range(-10,10),0);
                i += 1;
            }
            Destroy(attachedChains);
            Destroy(gameObject);
        }
        render.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        render.color = Color.white;
    }
}
