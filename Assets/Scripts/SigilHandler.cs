using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SigilHandler : MonoBehaviour
{
    public GameObject sensorPre;
    //public LineRenderer line;
    public List<GameObject> sensors;
    public int activeSensors;
    public int sensorBuffer;
    public bool immortality;
    public ParticleSystem ps;
    public Animator cameraAnim;
    public Transform sigilRender;
    public int enemyCount;
    public EnemySpawner spawner;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sensorBuffer = 0;
        for(var i = sensors.Count - 1; i > -1; i--){
            if(sensors[i].GetComponent<Sensor>().active){
                sensorBuffer += 1;
            }
        }
        activeSensors = sensorBuffer;
        if(activeSensors == sensors.Count && immortality == true){
            StartCoroutine(CompleteSigil());
            immortality = false;
            
        }
    }
    public void CreateSigil(int size, int amount)
    {
        enemyCount = 0;
        immortality = true;
        sensors.Clear();
        foreach(GameObject sensor in GameObject.FindGameObjectsWithTag("Sensor")){
            Destroy(sensor);
        }
        int i = 0;
        //line.positionCount = amount;
        GameObject currentSensor;
        while(i < amount){
            currentSensor = Instantiate(sensorPre, Random.insideUnitCircle * size, Quaternion.identity);
            sensors.Add(currentSensor);
            //line.SetPosition(i,currentSensor.transform.position);
            i += 1;
        }
        sigilRender.localScale = new Vector3(size*2.75f,size*2.75f, 1);
    }
    IEnumerator CompleteSigil()
    {
        Debug.Log("Sigil Filled");
        yield return new WaitForSeconds(1f);
        ps.Play(false);
        GameObject.Find("Flamethrower").GetComponent<PlayerAttack>().nextChains.Clear();
        cameraAnim.SetBool("sigil", true);
        yield return new WaitForSeconds(0.75f);
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            enemy.GetComponent<EnemyMove>().chained = false;
            enemy.layer = 8;
            enemy.GetComponent<AIPath>().canMove = true;
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            enemy.GetComponent<AIPath>().maxSpeed = 4 + spawner.difficulty / 5;
            Debug.Log("Enemy Freed");
        }
        foreach(GameObject chain in GameObject.FindGameObjectsWithTag("Chain")){
            Destroy(chain);
        }
        yield return new WaitForSeconds(0.75f);
        cameraAnim.SetBool("sigil", false);
        AstarPath.active.Scan();
    }
}