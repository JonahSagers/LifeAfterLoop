using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilHandler : MonoBehaviour
{
    public GameObject sensorPre;
    //public LineRenderer line;
    public List<GameObject> sensors;
    public int activeSensors;
    public int sensorBuffer;
    public bool immortality;
    public ParticleSystem Particle1;
    public ParticleSystem Particle2;
    public Animator cameraAnim;
    // Start is called before the first frame update
    void Awake()
    {
        immortality = true;
        CreateSigil(3,3);
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
    void CreateSigil(int size, int amount)
    {
        sensors.Clear();
        int i = 0;
        //line.positionCount = amount;
        GameObject currentSensor;
        while(i < amount){
            currentSensor = Instantiate(sensorPre, new Vector3(transform.position.x + Random.Range(-size, size),transform.position.y + Random.Range(-size, size),0), Quaternion.identity);
            sensors.Add(currentSensor);
            //line.SetPosition(i,currentSensor.transform.position);
            i += 1;
        }
        transform.localScale = new Vector3(size*3,size*3, 1);
    }
    IEnumerator CompleteSigil()
    {
        Debug.Log("Sigil Filled");
        Particle1.Play(false);
        yield return new WaitForSeconds(1f);
        cameraAnim.SetBool("sigil", true);
        Particle2.Play(false);
        yield return new WaitForSeconds(1.5f);
        cameraAnim.SetBool("sigil", false);
    }
}