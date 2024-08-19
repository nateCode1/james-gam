using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power : MonoBehaviour
{

    public Arm arm;

    public GameObject sprite;
    private SpriteRenderer spriterend;
    private Color fadeColor;
    private float pRed, pBlue, pGreen;

    private float width = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0f,1f,1f);
        spriterend = sprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        width = (arm.rotational_speed)/25;
        transform.localScale = new Vector3 (width,1,1);
        fadeColor = Color.Lerp(Color.green, Color.red, arm.rotational_speed/55);
        spriterend.color = fadeColor;
    }
}
