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
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    void Update()
    {
        healthBar.size = health;
    }

    // Update is called once per frame
    void FixedUpdate(){
        velX += Input.GetAxisRaw("Horizontal") * speed;
        velY += Input.GetAxisRaw("Vertical") * speed;
        rb.velocity += new Vector2(velX,velY);
        velX = VelocityClamp(velX);
        velY = VelocityClamp(velY);
        rb.velocity *= 0.85f;
    }

    public float VelocityClamp(float velocity){
        velocity *= 0.85f;
        if(velocity < 0.01f && velocity > -0.01f){
            velocity = 0;
        }
        return velocity;
    }

    

    public IEnumerator TakeDamage(float damage)
    {
        health -= damage;
        render.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        render.color = Color.white;
    }
}
