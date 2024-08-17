using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedDetector : MonoBehaviour
{
    public bool isGrounded = false;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 6) {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.layer == 6) {
            isGrounded = false;
        }
    }

}
