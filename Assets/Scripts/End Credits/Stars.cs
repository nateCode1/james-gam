using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{

    Vector3 velocity = new Vector3(0.0005f,-0.00005f,0);

    public float speedModifier = 1f;

    private void Awake() {
    }

    private void Start()
    {  
    }

    private void FixedUpdate() {
        transform.position += velocity * speedModifier;
    }

}
