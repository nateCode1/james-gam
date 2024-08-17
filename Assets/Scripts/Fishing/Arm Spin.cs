using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameArm : MonoBehaviour
{
    public float rotational_accel = 0.1f;
    public float rotational_speed = 0.0f;
    private Rigidbody2D _rigidbody;
    private Transform arm_movement;
    

    Vector3 rotation = new Vector3(0,0,1);

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        arm_movement = GetComponent<Transform>();
        }

    private void Start()
    {
        
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            rotational_speed = rotational_speed + rotational_accel;
        }
        transform.eulerAngles += rotation * rotational_speed;
        if (rotational_speed > 10000) {
            if(Input.GetButtonUp("mouse 0")) {
                _rigidbody.constraints = ~ (RigidbodyConstraints2D.FreezePositionX & RigidbodyConstraints2D.FreezePositionY);
            }
        }
    }

}
