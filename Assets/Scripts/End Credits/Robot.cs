using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float rotational_speed = 0.0f;
    private Rigidbody2D _rigidbody;

    Vector3 rotation = new Vector3(0,0,1);
    Vector3 velocity = new Vector3(0.0005f,-0.0001f,0);

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {  
    }

    private void FixedUpdate() {
        transform.eulerAngles -= rotation * rotational_speed;
        transform.position += velocity;
    }

}
