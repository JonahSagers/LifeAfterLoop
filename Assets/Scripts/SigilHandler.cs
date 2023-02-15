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
    // Start is called before the first frame update
    void Start()
    {
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
        if(activeSensors == sensors.Count){
            Debug.Log("Sigil Filled");
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
    }
}
