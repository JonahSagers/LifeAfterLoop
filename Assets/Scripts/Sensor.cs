using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public SpriteRenderer render;
    public List<Collider2D> overlaps;
    public bool active;
    public LayerMask chainedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(overlaps.Count > 0){
            active = true;
            render.color = Color.green;
        } else {
            active = false;
            render.color = Color.magenta;
        }
        if(Physics2D.OverlapCircleAll(transform.position,1,chainedEnemies).Length > 0){
            foreach(Collider2D enemy in Physics2D.OverlapCircleAll(transform.position,1,chainedEnemies)){
                if(!overlaps.Contains(enemy.gameObject.GetComponent<CircleCollider2D>())){
                    overlaps.Add(enemy.gameObject.GetComponent<CircleCollider2D>());
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        // if(hit.gameObject.layer == 8 && hit.gameObject.GetComponent<EnemyMove>().chained == true && !overlaps.Contains(hit.gameObject)){
        //     overlaps.Add(hit.gameObject.GetComponent<CircleCollider2D>());
        // }
    }
    void OnTriggerExit2D(Collider2D hit)
    {
        if(hit.gameObject.layer == 8){
            overlaps.Remove(hit.gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
