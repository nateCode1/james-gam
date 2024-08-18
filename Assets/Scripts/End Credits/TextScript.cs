using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Text credits;
    public Color fadeColor = new Color(0,0,0,1);
    public Color trans = new Color(0,0,0,0);
    public float fadeSpeed = 0.1f;

    void Start()
    {
        Text text = GetComponent<Text>();
        text.color = trans;
    }

    // Update is called once per frame
    void Update()
    {
        credits.color = Color.Lerp(credits.color, fadeColor, fadeSpeed * Time.deltaTime);
    }
}
