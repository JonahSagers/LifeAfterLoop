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
    public int chainCooldown;
    public GameObject chainshot;
    public GameObject projectile;
    public GameObject chainPre;
    public GameObject chain;
    public TextDisplay text;
    public List<GameObject> nextChains;
    public List<Vector2> edgePoints;
    public bool canFlame;
    public bool canChain;
    public Rigidbody2D playerRb;
    public Animator anim;
    public Transform player;
    public AudioSource speakerChain;
    public AudioSource speakerFlame;
    // Start is called before the first frame update
    void Start()
    {
        flamethrowerFuel = 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        mousePos2D = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2((mousePos2D - new Vector2(transform.position.x,transform.position.y)).y, (mousePos2D - new Vector2(transform.position.x,transform.position.y)).x)*(180/Mathf.PI)));
        transform.position += transform.right;
        if(Input.GetMouseButtonDown(1) && canFlame == true){
            ps.Play();
            flamethrower = true;
        } else if(Input.GetMouseButtonUp(1)){
            ps.Stop();
            flamethrower = false;
        }
        if(Input.GetMouseButton(0) && chainCooldown <= 0 && canChain == true){
            playerRb.AddForce(Vector3.Normalize((transform.position - GameObject.Find("Player").transform.position)) * -100);
            projectile = Instantiate(chainshot, transform.position + (transform.right/2), Quaternion.identity);
            projectile.GetComponent<Chainshot>().rb.velocity = transform.right * 10;
            anim.Play("Chain");
            speakerChain.Play();
            chainCooldown = 50;
        }
        if(flamethrower || chainCooldown > 40){
            cameraAnim.SetBool("flamethrower", true);
        } else {
            cameraAnim.SetBool("flamethrower", false);
        }
        anim.SetBool("Flamethrower", flamethrower);
        Fuelbar.size = flamethrowerFuel / 100;
    }
    
    void FixedUpdate()
    {
        if(flamethrower){
            if(flamethrowerFuel >= 1){
                flamethrowerFuel -= 0.5f;
                speakerFlame.pitch = flamethrowerFuel / 100;
                speakerFlame.Play();
                playerRb.AddForce(Vector3.Normalize((transform.position - GameObject.Find("Player").transform.position)) * -25);
            } else {
                ps.Stop();
                flamethrower = false;
            }
        } else if(flamethrowerFuel < 100){
            flamethrowerFuel += 1f;
        }
        if(chainCooldown > 0){
            chainCooldown -= 1;
        }
    }

    public void ChainEnemies()
    {
        for(var i = nextChains.Count - 1; i > -1; i--){
            if (nextChains[i] == null){
                nextChains.RemoveAt(i);
            }
        }
        if(nextChains.Count >= 2){
            text.tutorialTrigger = true;
            chain = Instantiate(chainPre);
            chain.transform.position = Vector3.zero;
            chain.GetComponent<LineRenderer>().positionCount = nextChains.Count;
            int i = 0;
            edgePoints.Clear();
            while(i < nextChains.Count){
                chain.GetComponent<LineRenderer>().SetPosition(i,nextChains[i].transform.position);
                edgePoints.Add(new Vector2(nextChains[i].transform.position.x,nextChains[i].transform.position.y));
                nextChains[i].GetComponent<EnemyMove>().attachedChains.Add(chain);
                i += 1;
            }
            chain.GetComponent<EdgeCollider2D>().SetPoints(edgePoints);
            for(int j = nextChains.Count; j > 1; j--){
                nextChains.RemoveAt(0);
                AstarPath.active.UpdateGraphs(chain.GetComponent<EdgeCollider2D>().bounds);
            }
        }
        
    }

    public void OnParticleCollision(GameObject hit)
    {
        if(hit.layer == 8 || hit.layer == 10){
            StartCoroutine(hit.GetComponent<EnemyMove>().TakeDamage(1));
        }
    }
}
