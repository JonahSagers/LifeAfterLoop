using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public SpriteRenderer render;
    public List<Sprite> sprites;
    public List<Collider2D> overlaps;
    public bool active;
    public LayerMask chainedEnemies;
    public SigilHandler sigil;
    public AudioSource speaker;
    // Start is called before the first frame update
    void Awake()
    {
        sigil = GameObject.Find("Sigil").GetComponent<SigilHandler>();
        render.sprite = sprites[Random.Range(0,sprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if(sigil.immortality == true){
            if(Physics2D.OverlapCircleAll(transform.position,1,chainedEnemies).Length > 0){
                foreach(Collider2D enemy in Physics2D.OverlapCircleAll(transform.position,1,chainedEnemies)){
                    if(!overlaps.Contains(enemy.gameObject.GetComponent<CircleCollider2D>())){
                        overlaps.Add(enemy.gameObject.GetComponent<CircleCollider2D>());
                        active = true;
                        speaker.Play();
                        render.color = Color.green;
                    }
                }
            } else{
                overlaps.Clear();
                active = false;
                render.color = Color.magenta;
            }
        } else {
            render.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        if(hit.gameObject.layer == 8){
            overlaps.Remove(hit.gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
