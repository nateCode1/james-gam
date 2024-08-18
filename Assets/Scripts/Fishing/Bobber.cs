using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private Spin spin;
    private HingeJoint2D hinge;
    private float velocity;

    private void Awake() {
    _rigidbody = GetComponent<Rigidbody2D>();
    spin = GetComponent<Spin>();
    hinge = GetComponent<HingeJoint2D>();
    }

    void Start()
    {
    }

    public void bobberReset(){
        transform.localEulerAngles = new Vector3(0,0,0);
        transform.parent = arm-cube;
        _rigidbody.constraints &= (RigidbodyConstraints2D.FreezePositionX & RigidbodyConstraints2D.FreezePositionY);
    }

    public void BobberRelease() {
        Destroy(hinge);
        velocity = spin.rotational_speed;
        transform.parent = null;
        _rigidbody.constraints &= (~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY);
        _rigidbody.velocity = new Vector3(velocity/(3*Mathf.Sqrt(2)),velocity/(3*Mathf.Sqrt(2)),0);
    }

    void FixedUpdate()
    {
    }
}
