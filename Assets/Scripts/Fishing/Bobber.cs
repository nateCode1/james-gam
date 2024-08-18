using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private Spin spin;
    private HingeJoint2D hinge;
    private float velocity;
    Vector3 startPos;
    Vector3 startRot;

    public GameObject parent;


    private void Awake() {
    _rigidbody = GetComponent<Rigidbody2D>();
    spin = GetComponent<Spin>();
    hinge = GetComponent<HingeJoint2D>();
    }

    void Start()
    {
        startPos = new Vector3(transform.position.x,transform.position.y,0);
        startRot = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y,transform.localEulerAngles.z);
    }

    public void BobberReset(){
        transform.position = startPos;
        transform.parent = parent.transform;
        transform.Rotate(Vector3.forward, 0.0f);
        _rigidbody.constraints = (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY);
    }

    public void BobberRelease() {
        velocity = spin.rotational_speed;
        transform.parent = null;
        _rigidbody.constraints &= (~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY);
        _rigidbody.velocity = new Vector3(velocity/(3*Mathf.Sqrt(2)),velocity/(3*Mathf.Sqrt(2)),0);
    }

    void FixedUpdate()
    {
    }
}
