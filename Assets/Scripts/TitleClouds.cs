using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleClouds : MonoBehaviour
{
    // Start is called before the first frame update

    private float position = 0.0f;
    public float speed = 0.05f;
    private float screenWidth = 2.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = (transform.position.x + speed * Time.deltaTime + screenWidth/2) % screenWidth - screenWidth/2;
        transform.position = new Vector3 (position, transform.position.y, transform.position.z);
    }
}
