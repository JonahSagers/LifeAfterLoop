using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float friction;
    public Image screenFlash;
    public Image screenFlashRed;
    public TextDisplay text;
    public bool gameOver;
    public PlayerAttack weapon;
    // Start is called before the first frame update
    void Start()
    {
        iFrames = 0;
        health = 100;
        friction = 0.81f;
        gameOver = false;
    }

    void Update()
    {
        healthBar.size = Mathf.Clamp(health / 100,0,1);
        if(health <= 0 && gameOver == false){
            StartCoroutine(GameOver());
        }
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(iFrames > 0){
            iFrames -= 1;
        }
        if(health < 100){
            health += 0.05f;
        }
        velX += Input.GetAxisRaw("Horizontal") * speed;
        velY += Input.GetAxisRaw("Vertical") * speed;
        rb.velocity += new Vector2(velX,velY);
        velX = VelocityClamp(velX);
        velY = VelocityClamp(velY);
        rb.velocity *= friction;
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
            iFrames = 10;
            health -= damage;
            render.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            render.color = Color.white;
        }
    }

    IEnumerator GameOver()
    {
        gameOver = true;
        friction = 0.95f;
        speed = 0;
        weapon.canChain = false;
        weapon.canFlame = false;
        screenFlashRed.enabled = true;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");
        asyncOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(0.1f);
        screenFlashRed.enabled = false;
        yield return new WaitForSeconds(1.85f);
        screenFlash.enabled = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(text.ShowText("Game Over", 1f));
        yield return new WaitForSeconds(1.39f);
        asyncOperation.allowSceneActivation = true;
        
    }
}
