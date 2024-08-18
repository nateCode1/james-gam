using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public float rotational_accel = 0.1f;
    public float rotational_speed = 0.0f;
    private Rigidbody2D _rigidbody;
    public bool isCasted = false;
    public bool didReset = true;

    public Bobber theBobber;
    private float startRot;

    Vector3 rotation = new Vector3(0,0,1);

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startRot = transform.eulerAngles.z;
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.Mouse0) && (!isCasted)) {
            rotational_speed = rotational_speed + rotational_accel;
        }
        transform.Rotate(Vector3.forward, -rotational_speed);
        if (rotational_speed > 60) {
            if(Input.GetMouseButtonUp(0)) {
                _rigidbody.constraints &= (~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY);
                _rigidbody.velocity = new Vector3(10,10,0);
            }
        }
        if(rotational_speed > 0){
            rotational_speed -= rotational_speed/1000;
            if(isCasted) {
                if(transform.eulerAngles.z % 360 == startRot) {
                    transform.eulerAngles = new Vector3 (0,0,startRot);
                    rotational_speed = 0.0f;
                }
            }
        }

        if(Input.GetMouseButtonUp(0)) {
            theBobber.BobberRelease();
            isCasted = true;
        }
    }

}
