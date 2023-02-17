using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHandler : MonoBehaviour
{
    public bool tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = true;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
