using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private float velocity;

    private void Awake() {
    _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    public void BobberRelease() {
        velocity = _rigidbody.angularVelocity;
        transform.parent = null;
        _rigidbody.constraints &= (~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY);
       _rigidbody.velocity = new Vector3(1,1,0);
    }

    void FixedUpdate()
    {
    }
}
