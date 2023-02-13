using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainshot : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if(hit.gameObject.layer == 8){
            hit.gameObject.GetComponent<EnemyMove>().chained = true;
            GameObject.Find("Flamethrower").GetComponent<PlayerAttack>().nextChains.Add(hit.gameObject);
            GameObject.Find("Flamethrower").GetComponent<PlayerAttack>().ChainEnemies();
        }
        Object.Destroy(gameObject);
    }
}
