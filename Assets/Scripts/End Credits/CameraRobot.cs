using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRobot : MonoBehaviour
{
    public float rotational_speed = 0.0f;
    private Rigidbody2D _rigidbody;
    private Transform arm_movement;

    Vector3 rotation = new Vector3(0,0,1);
    Vector3 velocity = new Vector3(0.0005f,-0.0001f,0);

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        arm_movement = GetComponent<Transform>();
    }

    private void Start()
    {  
    }

    private void FixedUpdate() {
        transform.eulerAngles -= rotation * rotational_speed;
        transform.position += velocity;

    }

}
