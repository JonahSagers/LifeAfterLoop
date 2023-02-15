using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public float velX;
    public float velY;
    public Rigidbody2D rb;
    public float speed;
    public SpriteRenderer render;
    public float health;
    public Scrollbar healthBar;
    public int iFrames;
    // Start is called before the first frame update
    void Start()
    {
        iFrames = 0;
        health = 100;
    }

    void Update()
    {
        healthBar.size = Mathf.Clamp(health / 100,0,1);
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(iFrames > 0){
            iFrames -= 1;
        }
        velX += Input.GetAxisRaw("Horizontal") * speed;
        velY += Input.GetAxisRaw("Vertical") * speed;
        rb.velocity += new Vector2(velX,velY);
        velX = VelocityClamp(velX);
        velY = VelocityClamp(velY);
        rb.velocity *= 0.81f;
    }

    public float VelocityClamp(float velocity){
        velocity *= 0.81f;
        if(velocity < 0.01f && velocity > -0.01f){
            velocity = 0;
        }
        return velocity;
    }

    public IEnumerator TakeDamage(float damage)
    {
        if(iFrames <= 0){
            iFrames = 20;
            health -= damage;
            render.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            render.color = Color.white;
        }
    }
}
