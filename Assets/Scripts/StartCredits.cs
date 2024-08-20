using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartCredits : MonoBehaviour
{

    public Image image;
    private bool fadeToBlack = false;
    // Start is called before the first frame update
    void Start()
    {
        image.color = new Color(0,0,0,0);
    }

    public void cannonTrigger(){
        fadeToBlack = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack) {
        image.color = Color.Lerp(image.color, Color.black, (float) 0.6 * Time.deltaTime);
        }
    }
}
