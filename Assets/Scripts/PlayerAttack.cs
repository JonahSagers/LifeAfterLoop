using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private Vector2 mousePos2D;
    public ParticleSystem ps;
    public float flamethrowerFuel;
    public bool flamethrower;
    public Animator cameraAnim;
    public Scrollbar Fuelbar;
    // Start is called before the first frame update
    void Start()
    {
        flamethrowerFuel = 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.zero;
        mousePos2D = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2((mousePos2D - new Vector2(transform.position.x,transform.position.y)).y, (mousePos2D - new Vector2(transform.position.x,transform.position.y)).x)*(180/Mathf.PI)));
        transform.localPosition = transform.right;
        if(Input.GetMouseButtonDown(1)){
            ps.Play();
            flamethrower = true;
        } else if(Input.GetMouseButtonUp(1)){
            ps.Stop();
            flamethrower = false;
        }
        cameraAnim.SetBool("flamethrower", flamethrower);
        Fuelbar.size = flamethrowerFuel / 100;
    }
    
    void FixedUpdate()
    {
        if(flamethrower){
            if(flamethrowerFuel >= 1){
                flamethrowerFuel -= 1;
            } else {
                ps.Stop();
                flamethrower = false;
            }
        } else if(flamethrowerFuel < 100){
            flamethrowerFuel += 1f;
        }
    }

    public void OnParticleCollision(GameObject hit)
    {
        if(hit.layer == 8){
            hit.GetComponent<EnemyMove>().StartCoroutine(hit.GetComponent<EnemyMove>().TakeDamage(1));
        }
    }
}
