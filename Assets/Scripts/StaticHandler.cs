using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHandler : MonoBehaviour
{
    public bool tutorial;
    private static GameObject handler;
    // Start is called before the first frame update
    void Awake()
    {
        if (handler == null){
            handler = gameObject;
        } else {
            Destroy(gameObject);
            tutorial = false;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("Static");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
