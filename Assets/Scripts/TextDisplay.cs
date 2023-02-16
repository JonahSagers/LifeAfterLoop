using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool texting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowText(string message)
    {
        int i = 0;
        while(i <= message.Length){
            text.text = message.Substring(0,i);
            i += 1;
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(1f);
        Debug.Log(i);
        while(i > 0){
            i -= 1;
            text.text = message.Substring(0,i);
            yield return new WaitForSeconds(0.025f);
        }
    }
}
