using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuitGame : MonoBehaviour
{
    public int quit;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color32(255,255,255,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            quit += 1;
            StartCoroutine(QuitCooldown());
        }
        if(quit >= 2){
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    IEnumerator QuitCooldown()
    {
        text.color = new Color32(255,255,255,255);
        yield return new WaitForSeconds(0.5f);
        float i = 0;
        while(i < 1){
            text.color = Color.Lerp(new Color32(255,255,255,255), new Color32(255,255,255,0), i);
            i += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        text.color = new Color32(255,255,255,0);
        quit -= 1;
    }
}
