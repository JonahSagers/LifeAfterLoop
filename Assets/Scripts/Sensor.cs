using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public SpriteRenderer render;
    public List<Collider2D> overlaps;
    // Start is called before the first frame update
    void Start()
    {
        render.color = Color.magenta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.layer == 8){
            overlaps.Add(hit.gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
