using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public float rotational_accel = 0.1f;
    public float rotational_speed = 0.0f;
    private Rigidbody2D _rigidbody;
    private bool isCasted = false;

    public Bobber theBobber;

    Vector3 rotation = new Vector3(0,0,1);

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {  
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.Mouse0) && (!isCasted)) {
            rotational_speed = rotational_speed + rotational_accel;
        }
        transform.eulerAngles -= rotation * rotational_speed;
        if (rotational_speed > 50) {
            if(Input.GetMouseButtonUp(0)) {
                _rigidbody.constraints &= (~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY);
                _rigidbody.velocity = new Vector3(10,10,0);
            }
        }
        if(rotational_speed > 0){
            rotational_speed -= rotational_speed/1000;
        }

        if(Input.GetMouseButtonUp(0)) {
            while (transform.eulerAngles.z % 360 > 5) {
                rotational_speed -= rotational_speed/200;
            }
            theBobber.BobberRelease();
            isCasted = true;
        }
    }

}
