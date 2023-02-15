using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public SpriteRenderer render;
    public List<Collider2D> overlaps;
    public bool active;
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
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.layer == 8){
            overlaps.Add(hit.gameObject.GetComponent<CircleCollider2D>());
        }
    }
    void OnTriggerExit2D(Collider2D hit)
    {
        if(hit.gameObject.layer == 8){
            overlaps.Remove(hit.gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
