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
    
    // Start is called before the first frame update
    void Start()
    {
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
        render.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        render.color = Color.white;
    }
}
